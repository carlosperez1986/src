using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using Modular.Core;
using Modular.Core.Data;
using Modular.Core.Localization;

namespace Modular.WebApplication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api"),
                a => a.Use(async (context, next) =>
                {
                    var principal = new ClaimsPrincipal();

                    var cookiesAuthResult = await context.AuthenticateAsync("Identity.Application");
                    if (cookiesAuthResult?.Principal != null)
                    {
                        principal.AddIdentities(cookiesAuthResult.Principal.Identities);
                    }

                    var bearerAuthResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                    if (bearerAuthResult?.Principal != null)
                    {
                        principal.AddIdentities(bearerAuthResult.Principal.Identities);
                    }

                    context.User = principal;
                    await next();
                }));

            return app;
        }

        public static IApplicationBuilder UseCustomizedMvc(this IApplicationBuilder app)
        {
            try
            {
                app.UseMvc(routes =>
                {
                    // routes.Routes.Add(new UrlSlugRoute(routes.DefaultHandler));

                    routes.MapRoute(
                        name: "areaRoute",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    routes.MapRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                });
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return app;
        }

        public static IApplicationBuilder UseCustomizedStaticFiles(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true,
                            MaxAge = TimeSpan.FromDays(-1)
                        };
                    }
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            Public = true,
                            MaxAge = TimeSpan.FromDays(60)
                        };
                    }
                });
            }

            return app;
        }

        public static IApplicationBuilder UseCustomizedRequestLocalization(this IApplicationBuilder app)
        {
            //string defaultCultureUI = GlobalConfiguration.DefaultCulture;
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var cultureRepository = scope.ServiceProvider.GetRequiredService<IRepositoryWithTypedId<Culture, string>>();
            //    GlobalConfiguration.Cultures = cultureRepository.Query().ToList();

            //    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            //    defaultCultureUI = config.GetValue<string>("Global.DefaultCultureUI");
            //}

            //var supportedCultures = GlobalConfiguration.Cultures.Select(c => c.Id).ToArray();
            //app.UseRequestLocalization(options =>
            //options
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures)
            //    .SetDefaultCulture(defaultCultureUI ?? GlobalConfiguration.DefaultCulture)
            //    .RequestCultureProviders.Insert(0, new RequestCultureProvider())
            //);

            return app;
        }
    }


}