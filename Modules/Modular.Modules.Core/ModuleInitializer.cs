using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Modular.Core.Modules;
using Modular.Modules.Core.Models;
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
            //serviceCollection.AddScoped<SignInManager<User>, SimplSignInManager<User>>();
        }

        //public void Init(IServiceCollection services)
        //{
        //    services.AddTransient<IAnotherTestService, AnotherTestService>();
        //    services.AddTransient<IPruebaC, Pruebas>();

        //}
    }
}
