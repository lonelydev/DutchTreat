using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
