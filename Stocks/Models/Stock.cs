using System.Collections.Generic;

namespace Stocks.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }

        public IEnumerable<ItemStockHistory> ItemsStocksHistory { get; set; }
    }
}
