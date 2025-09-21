//Stocknotification.cs
  
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock
{
    // Event arguments for stock threshold notifications
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }

        // Sets notification attributes
        public StockNotification(string stockName, int currentValue, int numChanges)
        {
            this.StockName = stockName;
            this.CurrentValue = currentValue;
            this.NumChanges = numChanges;
        }
    }
}
