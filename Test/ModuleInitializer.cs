using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Modular.Core.Modules;
using Modular.Modules.Core.Extensions;
using Modular.Modules.Core.Models;
using Modular.Modules.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ModuleInitializer : IModuleInitializer
{
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        //throw new NotImplementedException();
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITest, Test>();

    }

}
