using Nancy;
using System;
using System.Collections.Generic;
using System.Web;

namespace StockQuoteService
{
    public class Controller
    {
        public static Response Read(dynamic model)
        {
            Response response = Utilities.SerializeToJsonResponse(model);
            response.StatusCode = HttpStatusCode.OK;
            response.Headers.Add("Accept", "application/json");
            return response;
        }
    }
}