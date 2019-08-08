using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.WebHost.Extensions
{
    public class ModuleViewLocationExpander : IViewLocationExpander
    {
        private const string _moduleKey = "module";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            try
            {
                if (context.Values.ContainsKey(_moduleKey))
                {
                    var module = context.Values[_moduleKey];
                    if (!string.IsNullOrWhiteSpace(module))
                    {
                        var moduleViewLocations = new string[]
                        {
                            "/Modules/Modular.Modules." + module + "/Views/{1}/{0}.cshtml",
                            "/Modules/Modular.Modules." + module + "/Views/Shared/{0}.cshtml"
                        };

                        viewLocations = moduleViewLocations.Concat(viewLocations);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            try
            {
                var controller = context.ActionContext.ActionDescriptor.DisplayName;
                var moduleName = controller.Split('.')[2];
                if (moduleName != "WebApplication")
                {
                    context.Values[_moduleKey] = moduleName;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        
        }
    }
}
