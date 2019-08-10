﻿using ExtraDepenencyTest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Modular.Core.Modules;
using Modular.Modules.ModuleC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            serviceCollection.AddTransient<IPruebaC, Pruebas>();
        }

        //public void Init(IServiceCollection services)
        //{
        //    services.AddTransient<IAnotherTestService, AnotherTestService>();
        //    services.AddTransient<IPruebaC, Pruebas>();

        //}
    }
}
