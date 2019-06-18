using Newtonsoft.Json;

namespace Stocks.Models
{
    public class ItemState
    {
        public int Id { get; set; }
        public double Mass { get; set; }
        public int? DeviceId { get; set; }

        [JsonIgnore]
        public ItemStockHistory ItemStockHistory { get; set; }
    }
}
