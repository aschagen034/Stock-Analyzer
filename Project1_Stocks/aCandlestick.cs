using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Stocks
{   
        //Candlestick class
        public class Candlestick
        {
            //Properties for storing candlestick data
            public DateTime Date { get; set; }          //Date of the candlestick
            public decimal Open { get; set; }           //Opening price
            public decimal High { get; set; }           //Highest price
            public decimal Low { get; set; }            //Lowest price
            public decimal Close { get; set; }          //Closing price
            public decimal Volume { get; set; }         //Trading volume

            //Default constructor
            public Candlestick() {  }

            //Copy Constructor
            public Candlestick(Candlestick copy)
            {
                this.Open = copy.Open;          // Copy the open from candlestick passed
                this.High = copy.High;          // Copy the high from candlestick passed
                this.Low = copy.Low;            // Copy the low from candlestick passed
                this.Close = copy.Close;        // Copy the close from candlestick passed
                this.Volume = copy.Volume;      // Copy the volume from candlestick passed
                this.Date = copy.Date;          // Copy the date from candlestick passed
            }

        public Candlestick(string rowOfData)
        {
        }

        public Candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        // Determine if the candlestick is bullish
        public bool IsBullish()
        {
            return Close > Open;    //If it is bullish, the closing price is higher than the opening price
        }

        //Determine of the candlestick is bearish
        public bool IsBearish()
        {
            return Close < Open;   //If it is bearish, the closing price is lower than the opening price
        }

        //Override ToString to provide a formatted string representation of the candlestick's data
        public override string ToString()
            {
                // This method returns a string that include the date, opening price, highest price, lowest price, closing price, and volume
                return $"Candlestick(Date: {Date}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume})";
            }


        }
    }

