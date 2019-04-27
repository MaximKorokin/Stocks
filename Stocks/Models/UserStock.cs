using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Models
{
    public class UserStock
    {
        public int UserId { get; set; }
        public int StockId { get; set; }

        public User User { get; set; }
        public Stock Stock { get; set; }
    }
}
