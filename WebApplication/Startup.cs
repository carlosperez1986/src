using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using MediatR;
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

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;

            try
            {
                services.AddModules(_hostingEnvironment.ContentRootPath);

                services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            services.AddCustomizedDataStore(_configuration);

            services.AddCustomizedIdentity(_configuration);
            services.AddHttpClient();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));


            //services.LoadInstalledModules(modules, _hostingEnvironment);

            //services.AddCustomizedDataStore(Configuration);
            //services.AddCustomizedIdentity();

            services.AddCustomizedMvc(GlobalConfiguration.Modules);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
            });


            var sp = services.BuildServiceProvider();
            var moduleInitializers = sp.GetServices<Core.Modules.IModuleInitializer>();

            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.ConfigureServices(services);
            }

            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<IMediator, Mediator>();


            //string xx = _hostingEnvironment.WebRootPath;

            //var moduleRootFolder = _hostingEnvironment.ContentRootFileProvider.GetDirectoryContents("Modules");

            //foreach (var moduleFolder in moduleRootFolder.Where(x => x.IsDirectory))
            //{
            //    var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.PhysicalPath, "bin"));
            //    if (!binFolder.Exists)
            //    {
            //        continue;
            //    }

            //    foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            //    {
            //        try
            //        {
            //            Assembly assembly;

            //            try
            //            {
            //                assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
            //            }
            //            catch (FileLoadException ex)
            //            {
            //                assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
            //                if (assembly == null)
            //                {
            //                    throw;
            //                }

            //                string loadedAssemblyVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
            //                string tryToLoadAssemblyVersion = FileVersionInfo.GetVersionInfo(file.FullName).FileVersion;

            //                // Or log the exception somewhere and don't add the module to list so that it will not be initialized
            //                if (tryToLoadAssemblyVersion != loadedAssemblyVersion)
            //                {
            //                    throw new Exception($"Cannot load {file.FullName} {tryToLoadAssemblyVersion} because {assembly.Location} {loadedAssemblyVersion} has been loaded");
            //                }
            //                //if (ex.Message == "Assembly with same name is already loaded")
            //                //{
            //                //    continue;
            //                //}
            //                //throw;
            //            }

            //            if (assembly.FullName.Contains(moduleFolder.Name))
            //            {
            //                modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.PhysicalPath });
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            ex.Message.ToString();
            //        }

            //    }
            //}

            //var mvcBuilder = services.AddMvc();
            //var moduleInitializerInterface = typeof(IModuleInitializer);
            //foreach (var module in modules)
            //{
            //    // Register controller from modules
            //    mvcBuilder.AddApplicationPart(module.Assembly);

            //    // Registra los módulos para injectarlos // 
            //    try
            //    {
            //        var moduleInitializerType = module.Assembly.GetTypes().Where(x => typeof(IModuleInitializer).IsAssignableFrom(x)).FirstOrDefault();

            //        if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
            //        {
            //            var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
            //            moduleInitializer.Init(services);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.Message.ToString();
            //    }
            //}

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddCustomizedMvc(modules);

            //return services.Build(Configuration, _hostingEnvironment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseHttpsRedirection();
            app.UseCustomizedStaticFiles(env);
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimplCommerce API V1");
            //});

            app.UseCookiePolicy();
            app.UseCustomizedIdentity();
            app.UseCustomizedRequestLocalization();
            app.UseCustomizedMvc();

            var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.Configure(app, env);
            }

        }
    }
}
