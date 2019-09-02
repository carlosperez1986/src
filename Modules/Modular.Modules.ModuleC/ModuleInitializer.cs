using ExtraDepenencyTest;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Modular.Core.Interfaces;
using Modular.Core.Modules;
using Modular.Modules.Core.Services;
using Modular.Modules.ModuleC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleC
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAnotherTestService, AnotherTestService>();
            serviceCollection.AddScoped<Modular.Core.Interfaces.ISeessionData, Pruebas>();

            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });

            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            //serviceCollection.AddSingleton<DL.DL>();
        }

        //public void Init(IServiceCollection services)
        //{
        //    services.AddTransient<IAnotherTestService, AnotherTestService>();
        //    services.AddTransient<IPruebaC, Pruebas>();

        //}
    }
}
