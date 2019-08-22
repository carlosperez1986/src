using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core.Modules;
using Modular.Modules.Core.Services;

namespace Modular.Modules.EmailSMTP
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IEmailSender, EmailSmtp>();
            //serviceCollection.AddScoped<SignInManager<User>, SimplSignInManager<User>>();
            //serviceCollection.AddTransient<IEntityService, EntityService>();

            //serviceCollection.AddTransient<IThemeService, ThemeService>();
            //serviceCollection.AddTransient<ITokenService, TokenService>();
            //serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            //erviceCollection.AddScoped<IWorkContext, WorkContext>();
        }

        //public void Init(IServiceCollection services)
        //{
        //    services.AddTransient<IAnotherTestService, AnotherTestService>();
        //    services.AddTransient<IPruebaC, Pruebas>();

        //}
    }
}
