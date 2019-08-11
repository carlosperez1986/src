using ExtraDepenencyTest;
using Microsoft.AspNetCore.Mvc;
using Modular.Core.Data;
using Modular.Modules.ModuleC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleC.Controllers
{
    public interface IPruebaC
    {
        string PruebaC();
    }

    public class Pruebas : IPruebaC
    {
        public string PruebaC()
        {
            return "a1";
        }
    }


    public class TestCController : Controller
    {
        private IAnotherTestService _anotherTestService;
        private readonly IRepository<TestModels> _testModel;


        public TestCController(IAnotherTestService anotherTestService, IRepository<TestModels> testModel)
        {
            _testModel = testModel;
            _anotherTestService = anotherTestService;
        }

        public IActionResult Index()
        {

            var tModel = new TestModels { Text = "Decription X" };

            _testModel.Add(tModel);

            _testModel.SaveChange();
            var xx = _testModel.Query();

            ViewBag.AnotherTestData = _anotherTestService.Test() + " ";
            return View();
        }
    }
}
