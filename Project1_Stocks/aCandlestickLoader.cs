using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Project2_Stocks
{
    //Static class for loading candlestick data from a CSV file
    public static class CandlestickLoader
    {
        //Method to load candlestick data from a CSV file and return a list of Candlestick objects
        public static List<Candlestick> LoadFromCsv(string filePath)
        {
            //List to store each candlestick data object
            var candlesticks = new List<Candlestick>();

            //Open the specified CSV file for reading
            using (var reader = new StreamReader(filePath))
            {
                //Define delimiters for separating CSV values
                char[] delimiters = { ',', '"' };
                string line = reader.ReadLine();

                //Read each line of the CSV file until reaching the end
                while ((line = reader.ReadLine()) != null) 
                {
                    //Split the line by delimiters to extract individual values
                    var values = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);             

                    //Parse each field and covert to appropriate data types
                    var date = DateTime.ParseExact(values[0], "M/d/yyyy", CultureInfo.InvariantCulture);

                    //Parse and round decimal values for open, high, low, close, and volume prices
                    var open = Math.Round(100 * decimal.Parse(values[1], CultureInfo.InvariantCulture)) / 100; 
                    var high = Math.Round(100 * decimal.Parse(values[2], CultureInfo.InvariantCulture)) / 100;
                    var low = Math.Round(100 * decimal.Parse(values[3], CultureInfo.InvariantCulture)) / 100;
                    var close = Math.Round(100 * decimal.Parse(values[4], CultureInfo.InvariantCulture)) / 100;
                    var volume = Math.Round(100 * decimal.Parse(values[5], CultureInfo.InvariantCulture)) / 100;

                    //Create a Candlestick object and add it to the list
                    var candlestick = new Candlestick(date, open, high, low, close, volume);
                    candlesticks.Add(candlestick);
                }
            }
            //Return the list of candlestick objects
            return candlesticks;
        }
    }
}