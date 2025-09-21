//stockbroker.cs


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


namespace Stock
{
    // StockBroker subscribes to notifications and writes details asynchronously
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        private static readonly string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1output.txt");
        private static bool headerWritten = false;

        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        // Subscribe to stock notifications
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += EventHandler;
        }

        // Handle notifications asynchronously
        public async void EventHandler(object sender, StockNotification e)
        {
            await WriteNotificationAsync(e);
        }

        // Async output to console and file, with header
        private async Task WriteNotificationAsync(StockNotification e)
        {
            string header = "Broker".PadRight(16) + "Stock".PadRight(16) + "Value".PadRight(16) + "Changes".PadRight(16) + "Date and Time";
            string line = BrokerName.PadRight(16) + e.StockName.PadRight(16) + e.CurrentValue.ToString().PadRight(16) + e.NumChanges.ToString().PadRight(16) + DateTime.Now.ToString();

            if (!headerWritten)
            {
                Console.WriteLine(header);
                await File.AppendAllTextAsync(destPath, header + Environment.NewLine);
                headerWritten = true;
            }
            Console.WriteLine(line);
            await File.AppendAllTextAsync(destPath, line + Environment.NewLine);
        }
    }
}

