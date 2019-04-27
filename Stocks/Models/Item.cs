using System.Collections.Generic;

namespace Stocks.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Capacity { get; set; }

        public ItemState ItemState { get; set; }
        public IEnumerable<ItemStockHistory> ItemsStocksHistory { get; set; }
    }
}
