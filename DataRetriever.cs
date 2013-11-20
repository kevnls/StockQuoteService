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
                string reply = client.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=" + symbol + "&f=sd1c1pp2vjk");

                //clean up the string
                reply = reply.Replace("\"", "");
                reply = reply.Trim();

                string[] valuesArray = reply.Split(',');

                dynamic Object = new ExpandoObject();

                Object.Symbol = valuesArray[0];
                Object.LastTradeDate = valuesArray[1];
                Object.Change = valuesArray[2];
                Object.PreviousClose = valuesArray[3];
                Object.PercentChange = valuesArray[4];
                Object.Volume = valuesArray[5];
                Object.FiftyTwoWeekLow = valuesArray[6];
                Object.FiftyTwoWeekHigh = valuesArray[7];

                quotesArrayList.Add(Object);
            }
        }

        public dynamic[] ReturnData()
        {
            //return the in-memory quote array
            return quotesArrayList.ToArray();
        }

    }
}
