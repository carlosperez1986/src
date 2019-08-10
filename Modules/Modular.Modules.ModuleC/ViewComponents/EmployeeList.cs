using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Modular.Modules.ModuleC.Components
{
    //[ViewComponent(Name = "Footer")]
    [ViewComponent(Name = "EmployeeList1")]
    public class MoulecViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //ExampleModel model = new ExampleModel(2);

            // returns a fin

            /return Task.FromResult<IViewComponentResult>(View(model));
            return View();
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
