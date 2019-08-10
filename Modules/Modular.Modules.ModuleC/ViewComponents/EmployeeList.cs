using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Modular.Modules.ModuleC.Components
{
    //[ViewComponent(Name = "Footer")]
    [ViewComponent(Name = "EmployeeList")]
    public class EmployeeList : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(int test)
        {
            //ExampleModel model = new ExampleModel(2)

            // returns a fi

            //return Task.FromResult<IViewComponentResult>(View(model));
            return View(test);
        }
    }

    internal class ExampleModel
    {
        private int v;

        public ExampleModel(int v)
        {
            this.v = v;
        }
    }
}
