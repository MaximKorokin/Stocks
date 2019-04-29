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
        Stock AddStock(Stock stock, int ownerId);
        bool RemoveStock(int stockId, int ownerId);
        IEnumerable<Stock> GetStocks(int ownerId);
    }

    public class StocksService : IStocksService
    {
        private readonly StocksDbContext _db;

        public StocksService(StocksDbContext db)
        {
            _db = db;
        }

        public Stock AddStock(Stock stock, int ownerId)
        {
            if (stock != null)
            {
                _db.Stocks.Add(stock);
                _db.SaveChanges();
                _db.UsersStocks.Add(new UserStock { StockId = stock.Id, UserId = ownerId });
                _db.SaveChanges();
                return stock;
            }
            return null;
        }

        public bool RemoveStock(int stockId, int ownerId)
        {
            var userStock = _db.UsersStocks.FirstOrDefault(us => us.StockId == stockId && us.UserId == ownerId);

            if (userStock == null)
                return false;

            var stock = _db.Stocks.Find(userStock.StockId);

            if (stock != null)
            {
                _db.Stocks.Remove(stock);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Stock> GetStocks(int ownerId)
        {
            return _db.UsersStocks
                .Where(us => us.UserId == ownerId)
                .Include(us => us.Stock)
                .Select(us => us.Stock);
        }
    }
}
