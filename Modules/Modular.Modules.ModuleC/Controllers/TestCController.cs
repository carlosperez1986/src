using ExtraDepenencyTest;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Modular.Core.Data;
using Modular.Core.Interfaces;
using Modular.Modules.Core.Controllers;
using Modular.Modules.Core.Services;
using Modular.Modules.ModuleC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleC.Controllers
{
    public class Notifier2 : INotificationHandler<NotificationMessage>
    {
        public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            Console.Write($"Debugging from Notifier 222222. Message  : {notification.NotifyText.Session.GetString("hola")} ");
            return Task.CompletedTask;
        }
    }


    public class TestCController : Controller
    {
        private IAnotherTestService _anotherTestService;
        private readonly IRepository<TestModels> _testModel;
        private IEmailSender _EMAIL;
        private readonly ISeessionData _sdata;
        private IHttpContextAccessor _httpcontext;

        public TestCController(ISeessionData sdata, IHttpContextAccessor httpcontext, IEmailSender EMAIL, IAnotherTestService anotherTestService, IRepository<TestModels> testModel)
        {
            _testModel = testModel;
            _anotherTestService = anotherTestService;
            _EMAIL = EMAIL;
            _httpcontext = httpcontext;
            _sdata = sdata;
        }

        public IActionResult Index()
        {

            string xccx = _sdata.PruebaC1();

            _EMAIL.SendEmailAsync("", "", "", false);
            //var tModel = new TestModels { Text = "Decription X" };

            //_testModel.Add(tModel);

            //_testModel.SaveChange();

            _httpcontext.HttpContext.Session.SetString("hola", "xxxx");

            TestModels m = new TestModels();
            m.IdTest = 123123;

            var xx = _testModel.Query();

            ViewBag.AnotherTestData = _anotherTestService.Test() + " " + xx.Count();

            return View(m);
        }
    }
}
