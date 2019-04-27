namespace Stocks.Models
{
    public class ItemState
    {
        public int Id { get; set; }
        public double Mass { get; set; }
        
        public ItemStockHistory ItemStockHistory { get; set; }
    }
}
