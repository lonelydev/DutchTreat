using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      // Please make db context as part of the service collection 
      // So that i can access it from a controller
      services.AddDbContext<DutchContext>(cfg => cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString")));
      //services.AddScoped - lives for the length of request
      //services.AddSingleton - lives for as long as the program is alive
      services.AddTransient<IMailService, NullMailService>();
      //support for real mail service

      /*Requires to use Dependency Injection!*/
      services.AddMvc();
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
      app.UseMvc(cfg =>
     {
             /* default behaviour is to direct the site to App controller's Index view*/
       cfg.MapRoute("Default",
                 "{controller}/{action}/{id?}",
                 new { controller = "App", Action = "Index" });
     });
    }
  }
}
