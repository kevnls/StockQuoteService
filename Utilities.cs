using Nancy;
using Nancy.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;

namespace StockQuoteService
{
    public static class Utilities
    {
        //turns an IDictionary into an ExpandoObject
        public static ExpandoObject DictionaryToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            foreach (var item in dictionary)
            {
                bool alreadyProcessed = false;

                if (item.Value is IDictionary<string, object>)
                {
                    expandoDic.Add(item.Key, DictionaryToExpando((IDictionary<string, object>)item.Value));
                    alreadyProcessed = true;
                }
                else if (item.Value is ICollection)
                {
                    var itemList = new List<object>();
                    foreach (var item2 in (ICollection)item.Value)
                        if (item2 is IDictionary<string, object>)
                            itemList.Add(DictionaryToExpando((IDictionary<string, object>)item2));
                        else
                            itemList.Add(DictionaryToExpando(new Dictionary<string, object> { { "Unknown", item2 } }));

                    if (itemList.Count > 0)
                    {
                        expandoDic.Add(item.Key, itemList);
                        alreadyProcessed = true;
                    }
                }

                if (!alreadyProcessed)
                    expandoDic.Add(item);
            }

            return expando;
        }

        //will attempt to serialize an object of any type
        public static Response SerializeToJsonResponse(dynamic input)
        {
            Nancy.Json.JavaScriptSerializer ser = new JavaScriptSerializer();
            var response = (Response)ser.Serialize(input);
            response.ContentType = "application/json";
            return response;
        }

        //will deserialize json into an expando object
        public static ExpandoObject DeserializeJsonToExpando(string json)
        {
            Nancy.Json.JavaScriptSerializer ser = new Nancy.Json.JavaScriptSerializer();
            var dictionary = ser.Deserialize<IDictionary<string, object>>(json);
            return dictionary.DictionaryToExpando();
        }
    }
}