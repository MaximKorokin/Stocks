using Newtonsoft.Json;
using System;

namespace Stocks.Models
{
    public class ItemStockHistory
    {
        public int ItemId { get; set; }
        public int StockId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int? ItemStateId { get; set; }

        [JsonIgnore]
        public Item Item { get; set; }
        [JsonIgnore]
        public Stock Stock { get; set; }
        [JsonIgnore]
        public ItemState ItemState { get; set; }
    }
}
