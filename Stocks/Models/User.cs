using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stocks.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Language { get; set; }

        [JsonIgnore]
        public IEnumerable<UserStock> UsersStocks { get; set; }
    }
}
