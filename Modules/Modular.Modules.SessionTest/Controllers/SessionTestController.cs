using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modular.Core.Interfaces;

namespace Modular.Modules.SessionTest.Controllers
{
    public class SessionTestController : Controller
    {
        private readonly IMediator _IMediator;
        private readonly IEnumerable<INotificationHandler<NotificationMessage>> _notifiers;


        public class Notifier3 : INotificationHandler<NotificationMessage>
        {
            public static string _logger;

            public Notifier3(string logger)
            {
                _logger = logger;
            }

            public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
            {

                _logger = notification.NotifyText.Session.GetString("hola");

                Console.Write($"Debugging from Notifier 4444. Message  : {_logger} ");

                return Task.CompletedTask;
            }
        }
        public SessionTestController(IMediator IMediator, IEnumerable<INotificationHandler<NotificationMessage>> notifiers)
        {
            _IMediator = IMediator;
            _notifiers = notifiers;
        }

        public IActionResult Index()
        {
            string xxd = Notifier3._logger;

            return View();
        }



    }
}
