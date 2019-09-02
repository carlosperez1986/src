using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Modular.Core.Interfaces
{
    public class NotificationMessage : INotification
    {
        public HttpContext NotifyText { get; set; }
    }

    public interface ISeessionData
    {
        void PruebaC();
        string PruebaC1();
    }

    public class Pruebas : ISeessionData
    {
        private IHttpContextAccessor _httpcontext;

        public Pruebas(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;

        }

        public void PruebaC()
        {

            _httpcontext.HttpContext.Session.SetString("hola1", "xxxx");

        }

        public string PruebaC1()
        {
            string xx = _httpcontext.HttpContext.Session.GetString("hola1");

            return xx;
        }


    }

}
