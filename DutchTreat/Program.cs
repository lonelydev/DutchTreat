using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
  public class Program
  {
    /// <summary>
    /// Just like a console app it is self hosted. 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration(SetupConfiguration)
            .UseStartup<Startup>()
            .Build();

    /// <summary>
    /// To configure our project, we are going to go hardcore, to add configuration manually.
    /// We use the builder object and use the methods on it to add configuration files.
    /// In case of conflicts in values among different configuration files, the order of 
    /// precedence depends on the order in which you add it.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="builder"></param>
    private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
    {
      //remove default configuration objects so that we can customize this to the core
      builder.Sources.Clear();

      //asp.net used to use web.config but this was cumbersome.
      //aspnetcore however provides a more flexible configuration system
      //several different types of configuration options available
      builder.AddJsonFile("config.json", optional: false, reloadOnChange: true)
        //.AddXmlFile("config.xml", optional: true)
        .AddEnvironmentVariables();
    }
  }
}
