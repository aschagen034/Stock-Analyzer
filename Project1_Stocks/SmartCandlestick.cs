using System;
using System.Collections.Generic;

namespace Project2_Stocks
{
    // The SmartCandlestick class extends the Candlestick class with additional derived properties and methods
    public class SmartCandlestick : Candlestick
    {
        // Derived properties representing additional characteristics of a candlestick
        public decimal Range { get; set; }        // Total range of the candlestick (High - Low)
        public decimal BodyRange { get; set; }   // Range of the candlestick body (|Close - Open|)
        public decimal TopPrice { get; set; }    // Higher value between Open and Close
        public decimal BottomPrice { get; set; } // Lower value between Open and Close
        public decimal UpperTail { get; set; }   // Length of the upper tail (High - TopPrice)
        public decimal LowerTail { get; set; }   // Length of the lower tail (BottomPrice - Low)

        // Method to calculate and populate all derived properties
        private void CalculateProperties()
        {
            Range = High - Low;                          // Calculate the total range
            BodyRange = Math.Abs(Close - Open);          // Calculate the body range
            TopPrice = Math.Max(Open, Close);            // Determine the top price (highest of Open or Close)
            BottomPrice = Math.Min(Open, Close);         // Determine the bottom price (lowest of Open or Close)
            UpperTail = High - TopPrice;                 // Calculate the upper tail length
            LowerTail = BottomPrice - Low;               // Calculate the lower tail length
        }

        // Constructor: Initializes a new SmartCandlestick based on an existing Candlestick
        public SmartCandlestick(Candlestick candlestick)
            : base(candlestick.Date, candlestick.Open, candlestick.High, candlestick.Low, candlestick.Close, candlestick.Volume)
        {
            CalculateProperties(); // Calculate derived properties upon instantiation
        }

        // Boolean properties for identifying candlestick patterns

        public bool IsBullish => Close > Open;       // True if the Close price is higher than the Open price
        public bool IsBearish => Close < Open;       // True if the Close price is lower than the Open price
        public bool IsNeutral => Close == Open;      // True if the Close price equals the Open price
        public bool IsMarubozu => Open == Low && Close == High; // True if the candlestick has no tails (Open = Low, Close = High)
        public bool IsHammer => LowerTail > 2 * BodyRange && UpperTail < BodyRange; // True if the candlestick forms a Hammer pattern
        public bool IsDoji => (double)BodyRange < (double)(High - Low) * 0.1;       // True if the body is very small compared to the range
        public bool IsDragonflyDoji => Open == Close && Open == Low;               // True if the Open and Close are equal and at the Low
        public bool IsGravestoneDoji => Open == Close && Open == High;             // True if the Open and Close are equal and at the High

        public string IdentifyPattern()
        {
            if (IsBullish) return "Bullish";
            if (IsBearish) return "Bearish";
            if (IsMarubozu) return "Marubozu";
            if (IsHammer) return "Hammer";
            if (IsDragonflyDoji) return "Dragonfly Doji";
            if (IsGravestoneDoji) return "Gravestone Doji";
            if (IsDoji) return "Doji";
            return "Neutral";
        }

      
        // Method to check if a candlestick forms a peak compared to its neighbors
        public bool IsPeak(List<SmartCandlestick> candlesticks, int index)
        {
            // Ensure the index is within valid range
            if (index <= 0 || index >= candlesticks.Count - 1) return false;

            // True if the current candlestick's High is greater than its neighbors' High
            return candlesticks[index].High > candlesticks[index - 1].High && candlesticks[index].High > candlesticks[index + 1].High;
        }

        // Method to check if a candlestick forms a valley compared to its neighbors
        public bool IsValley(List<SmartCandlestick> candlesticks, int index)
        {
            // Ensure the index is within valid range
            if (index <= 0 || index >= candlesticks.Count - 1) return false;

            // True if the current candlestick's Low is less than its neighbors' Low
            return candlesticks[index].Low < candlesticks[index - 1].Low && candlesticks[index].Low < candlesticks[index + 1].Low;
        }
    }
}
