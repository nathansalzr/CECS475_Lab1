//stock.cs

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Stock
{
    // Simulates a stock whose value changes on a background thread
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent; // Event for threshold notification

        private string name;
        private int initialValue;
        private int maxChange;
        private int threshold;
        private int numChanges;
        private int currentValue;
        private readonly Thread thread;

        public string StockName { get => name; set => name = value; }
        public int InitialValue => initialValue;
        public int CurrentValue => currentValue;
        public int MaxChange => maxChange;
        public int Threshold => threshold;
        public int NumChanges => numChanges;

        // Initialize stock and start simulation thread
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            this.name = name;
            this.initialValue = startingValue;
            this.currentValue = startingValue;
            this.maxChange = maxChange;
            this.threshold = threshold;
            this.numChanges = 0;
            this.thread = new Thread(new ThreadStart(Activate));
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        // Simulates the stock changing value every 500ms
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500);
                ChangeStockValue();
            }
        }

        // Updates stock value and fires event if out of threshold
        public void ChangeStockValue()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            int change = rand.Next(1, maxChange + 1);
            if (rand.Next(2) == 0) change = -change;
            currentValue += change;
            numChanges++;
            if (Math.Abs(currentValue - initialValue) > threshold)
            {
                StockEvent?.Invoke(this, new StockNotification(StockName, currentValue, numChanges));
            }
        }
    }
}

