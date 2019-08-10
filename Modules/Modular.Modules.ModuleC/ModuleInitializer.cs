﻿using ExtraDepenencyTest;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Modular.Modules.ModuleC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleC
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Init(IServiceCollection services)
        {
            services.AddTransient<IAnotherTestService, AnotherTestService>();
            services.AddTransient<IPruebaC, Pruebas>();

        }
    }
}
