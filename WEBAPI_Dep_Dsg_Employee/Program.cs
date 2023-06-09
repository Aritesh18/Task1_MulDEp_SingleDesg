
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging) =>
            {
              logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                   logging.AddDebug();
              logging.AddNLog();
              logging.AddEventSourceLogger();
            }).UseStartup<Startup>();
  }

}
