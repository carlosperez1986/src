using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modular.Modules.Core.Extensions;
using Modular.WebApplication;


namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void SetupConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder configBuilder)
        {
            string cnn = "Data Source=82.223.108.84,10606;Initial Catalog=shopcartdb;User ID=sa;Password=Bp9Ea51VGI;Packet Size=4096;MultipleActiveResultSets=True;Application Name=EntityFramework";

            var env = hostingContext.HostingEnvironment;
            var configuration = configBuilder.Build();
            configBuilder.AddEntityFrameworkConfig(options =>
                    options.UseSqlServer(cnn)
            );
            //Log.Logger = new LoggerConfiguration()
            //           .ReadFrom.Configuration(configuration)
            //           .CreateLogger();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(SetupConfiguration);
    }
}
