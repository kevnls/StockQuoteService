StockQuoteService
=================

REST service written in C# to cache and serve stock data from Yahoo

This project uses .NET 4.5 and NancyFX.  It reads stock data from Yahoo at a configurable interval and caches it in-memory.  You can then request the data via a standard GET call and you will get an array of JSON objects back that represent the quotes for the symbols you setup to be cached.  

You can reconfigure the quote-data you get back by changing the URL in the DataRetriever class.  Configuration for the URL is documented here:  http://www.gummy-stuff.org/Yahoo-data.htm
