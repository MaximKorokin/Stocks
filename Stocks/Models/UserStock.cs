using Newtonsoft.Json;
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

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Stock Stock { get; set; }
    }
}
