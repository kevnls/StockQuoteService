using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web;

namespace StockQuoteService
{
    public class Model
    {
        public dynamic[] Quotes()
        {
            DataRetriever dr = new DataRetriever();
            return dr.ReturnData();
        }
    }
}