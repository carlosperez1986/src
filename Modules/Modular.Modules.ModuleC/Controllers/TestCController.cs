﻿using ExtraDepenencyTest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleC.Controllers
{
    public class TestCController : Controller
    { 
        private IAnotherTestService _anotherTestService;

        public TestCController(IAnotherTestService anotherTestService)
        {
           
            _anotherTestService = anotherTestService;
        }

        public IActionResult Index()
        { 
            ViewBag.AnotherTestData = _anotherTestService.Test();
            return View();
        }
    }
}