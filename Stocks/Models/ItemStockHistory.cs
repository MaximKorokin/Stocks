using System;

namespace Stocks.Models
{
    public class ItemStockHistory
    {
        public int ItemId { get; set; }
        public int StockId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int? ItemStateId { get; set; }

        public Item Item { get; set; }
        public Stock Stock { get; set; }
        public ItemState ItemState { get; set; }
    }
}
