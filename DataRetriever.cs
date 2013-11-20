using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Timers;

namespace StockQuoteService
{
    public class DataRetriever
    {
        private string[] symbolsArray;
        private int retreivalHourInterval;

        //we're just going to keep our quotes in a static in-memory array
        private static ArrayList quotesArrayList = new ArrayList();

        public DataRetriever()
        {

        }

        public string[] Symbols
        {
            set
            {
                symbolsArray = value;
            }
        }

        public int RetrievalHourInterval
        {
            set
            {
                retreivalHourInterval = value;
            }
        }

        public void Start()
        {
            RetrieveData();
            double interval = retreivalHourInterval * 3600000;
            Timer timer = new Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            RetrieveData();
        }

        public void RetrieveData()
        {
            //empty quote array
            quotesArrayList.Clear();
            
            //fill quote array
            foreach (string symbol in symbolsArray)
            {
                WebClient client = new WebClient();
                string reply = client.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=" + symbol + "&f=sd1opjk");

                //clean up the string
                reply = reply.Replace("\"", "");
                reply = reply.Trim();

                string[] valuesArray = reply.Split(',');

                dynamic expObject = new ExpandoObject();

                expObject.Symbol = valuesArray[0];
                expObject.LastTradeDate = valuesArray[1];
                expObject.Open = valuesArray[2];
                expObject.PreviousClose = valuesArray[3];
                expObject.FiftyTwoWeekLow = valuesArray[4];
                expObject.FiftyTwoWeekHigh = valuesArray[5];

                quotesArrayList.Add(expObject);
            }
        }

        public dynamic[] ReturnData()
        {
            //return the in-memory quote array
            return quotesArrayList.ToArray();
        }

    }
}
