﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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

namespace Modular.Modules.Core
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SignInManager<User>, SimplSignInManager<User>>();
            serviceCollection.AddTransient<IEntityService, EntityService>();
            serviceCollection.AddSingleton<SettingDefinitionProvider>();
            serviceCollection.AddScoped<ISettingService, SettingService>();
            //serviceCollection.AddTransient<IThemeService, ThemeService>();
            //serviceCollection.AddTransient<ITokenService, TokenService>();
            //serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            serviceCollection.AddScoped<IWorkContext, WorkContext>();
        }

    }
}
