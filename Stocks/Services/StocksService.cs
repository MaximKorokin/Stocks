using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Services
{
    public interface IStocksService
    {
        Stock AddStock(Stock stock);
        bool DeleteSock(Stock user);
        IEnumerable<Stock> GetStocks(int userId);
    }

    public class StocksService : IStocksService
    {
        private readonly StocksDbContext _db;

        public StocksService(StocksDbContext db)
        {
            _db = db;
        }

        public Stock AddStock(Stock stock)
        {
            if (stock != null)
            {
                _db.Stocks.Add(stock);
                _db.SaveChanges();
                return stock;
            }
            return null;
        }

        public bool DeleteSock(Stock user)
        {
            return true;
        }

        public IEnumerable<Stock> GetStocks(int userId)
        {
            return _db.UsersStocks
                .Where(us => us.UserId == userId)
                .Include(us => us.Stock)
                .Select(us => us.Stock);
        }
    }
}
