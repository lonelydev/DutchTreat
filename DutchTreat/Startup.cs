using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace DutchTreat
{
  public class Startup
  {
    private readonly IConfiguration _config;
    private readonly IHostingEnvironment _environment;

    public Startup(IConfiguration config, IHostingEnvironment environment)
    {
      _config = config;
      _environment = environment;
    }

    /// <summary>
    /// Scoped service vs Transient service
    /// Transient - services that are created as needed, they are usually 
    /// stateless
    /// Scoped - DBContext is by default added as this. Cached and reused within
    /// scope. With each request, a scope is begun and ended.
    /// Singleton - created once and shared throughout the application
    /// </summary>
    /// <param name="services"></param>
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      // many more options available for configuration
      // in this one we are only going to use unique email configuration option
      // AddIdentity has fluent interface. so you can tell it where the data is stored
      // a lot of applications separate out identity context and app data context.
      // as this is a fairly simple one, we have just combined it into the same
      // Also makes sense here as Identity and Orders have a relationship in our application.
      services.AddIdentity<StoreUser, IdentityRole>(cfg =>
      {
        cfg.User.RequireUniqueEmail = true;
      }).AddEntityFrameworkStores<DutchContext>();

      // cookie auth and token auth
      services.AddAuthentication().AddCookie().AddJwtBearer(
        cfg =>
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidIssuer = _config["Tokens:Issuer"],
          ValidAudience = _config["Tokens:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
        }
      );

      // Please make db context as part of the service collection 
      // So that i can access it from a controller
      services.AddDbContext<DutchContext>(cfg => cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString")));
      // without mapper.reset, running dotnet ef database drop would fail with the following error:
      // Application startup exception: System.InvalidOperationException: Mapper already initialized. You must call Initialize once per application domain/process.
      Mapper.Reset();
      services.AddAutoMapper();
      //services.AddScoped - lives for the length of request
      //services.AddSingleton - lives for as long as the program is alive
      services.AddTransient<IMailService, NullMailService>();
      services.AddTransient<DutchSeeder>();

      // add IDutchRepository with implementation DutchRepository
      services.AddScoped<IDutchRepository, DutchRepository>();

      /*Requires to use Dependency Injection!
       * how to enable https in production
       */
      services.AddMvc(opt =>
      {
        if (_environment.IsProduction())
        {
          opt.Filters.Add(new RequireHttpsAttribute());
        }
      }).AddJsonOptions(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // the order of everything you do inside this function matters. the pipeline of handling matters 
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      //app.Run(async (context) =>
      //{
      //    await context.Response.WriteAsync("Hello World!");
      //});

      // to set a default home file.
      // index.html is a default file that microsoft supports
      // order is important - default file should be first. else file cannot find. 
      //app.UseDefaultFiles();

      //asks the server to serve static files that are in www root
      //whenever an exception is thrown, show the full stack trace for information. 
      // a naive way to only show this to developers is to ensure you do this like 
      // #if debug pragma
      // not a great idea, instead use 

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/error");
      }

      app.UseStaticFiles();

      // this is where you turn on the authentication!
      // this has to be done before your MVC is initialized
      app.UseAuthentication();

      app.UseMvc(cfg =>
     {
       /* default behaviour is to direct the site to App controller's Index view*/
       cfg.MapRoute("Default",
                 "{controller}/{action}/{id?}",
                 new { controller = "App", Action = "Index" });
     });

      // Only do seeding in development. protect production
      if (env.IsDevelopment())
      {
        using (var scope = app.ApplicationServices.CreateScope())
        {
          //creates a dutchseeder instance with prerequisites if any
          var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
          //making configure wait for seed to finish
          //cannot make Configure async as it isn't expected to run asynchronously
          seeder.Seed().Wait();
        }
      }
    }
  }
}
