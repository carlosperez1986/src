using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Modular.Core;
using Modular.Core.Data;
using Modular.Core.Modules;
using Modular.Modules.Core;
using Modular.Modules.Core.Data;
using Modular.Web.Extensions;
using Modular.WebApplication.Extensions;


namespace Modular.WebApplication
{
    public class Startup
    {
        private readonly IList<ModuleInfo> modules = new List<ModuleInfo>();

        // This method gets called by the runtime. Use this method to add services to the containerprivate readonly IList<ModuleInfo> modules = new List<ModuleInfo>();

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        readonly string CorsAllowAll = "_CorsAllowAll";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            //string projectDirectory = Directory.GetParent(WebRootPath).Parent.FullName;
            //GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath.Replace("Modular.WebHostApp","");
            GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;

            try
            {
                services.AddModules(GlobalConfiguration.ContentRootPath);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            //services.AddCustomizedDataStore(_configuration);

            //services.AddCustomizedIdentity(_configuration);
            services.AddHttpClient();

            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //services.AddTransient(typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));


            //services.AddCustomizedDataStore(Configuration);
            //services.AddCustomizedIdentity();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(CorsAllowAll,
            //    builder =>
            //    {
            //        builder.AllowAnyHeader()
            //               .AllowAnyMethod()
            //               .AllowAnyOrigin();
            //    });
            //});

            services.AddCustomizedMvc(GlobalConfiguration.Modules);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false; // Default is true, make it false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            //usado para renderizar HTML//
            // var invoiceHtml = await _viewRender.RenderViewToStringAsync("/InvoicePdf.cshtml", model);
            //services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300);//You can set Time  
            });

            services.AddHttpContextAccessor();

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-Token");

            var sp = services.BuildServiceProvider();
            var moduleInitializers = sp.GetServices<IModuleInitializer>();

            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.ConfigureServices(services);
            }

            services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
            //services.AddSingleton<Core.Helpers.SessionHelper>();
            //services.AddTransient<SetViewDataFilter>();
            //services.AddScoped<ServiceFactory>(p => p.GetService);
            //services.AddScoped<IMediator, Mediator>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(o => o.LoginPath = new PathString("/account/login"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseWhen(
                    context => !context.Request.Path.StartsWithSegments("/api"),
                    a => a.UseExceptionHandler("/Home/Error")
                );
                app.UseHsts();
            }

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api"),
                a => a.UseStatusCodePagesWithReExecute("/Home/ErrorWithCode/{0}")
            );

            //app.UseCors(CorsAllowAll);

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseAuthentication();
            //app.UseIdentity();
            //app.UseCustomizedStaticFiles(env);
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimplCommerce API V1");
            //});


            foreach (var module in GlobalConfiguration.Modules)
            {
                try
                {
                    var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path.Replace("WebApplication", ""), "wwwroot"));
                    //var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path.Replace("Modular.WebHostApp", ""), "Clases"));

                    if (!wwwrootDir.Exists)
                    {
                        continue;
                    }

                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(GlobalConfiguration.ContentRootPath, wwwrootDir.FullName)),
                        RequestPath = new PathString("")
                    });
                }
                catch (Exception)
                {

                    throw;
                }

            }

            app.UseSession();
            app.UseCookiePolicy();
            //app.UseCustomizedIdentity();
            app.UseCustomizedRequestLocalization();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            //app.UseCustomizedMvc();

            var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.Configure(app, env);

            }
        }

    }
}