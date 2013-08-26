using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;

namespace StockQuoteService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //kick off the DataRetriever
            DataRetriever dr = new DataRetriever();
            string symbolsList = ConfigurationManager.AppSettings["SymbolsList"];
            string[] symbolsArray = symbolsList.Split(',');
            dr.Symbols = symbolsArray;
            dr.RetrievalHourInterval = int.Parse(ConfigurationManager.AppSettings["RetrievalHourInterval"]);
            dr.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}