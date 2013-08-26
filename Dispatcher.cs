using Nancy;
using System;
using System.Collections.Generic;
using System.Web;

namespace StockQuoteService
{
    public class Dispatcher : NancyModule
    {
        public Dispatcher() : base()
        {
            //READ
            Get["/"] = parameters =>
            {
                dynamic model = new Model().Quotes();
                return Controller.Read(model);
            };
        }
    }
}