using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Services
{
    public interface IItemsService
    {
        Item AddItem(Item item, int stockId, int ownerId);
        Item GetItem(int itemId, int ownerId);
        IEnumerable<Item> GetStockItems(int stockId, int ownerId);
        bool RemoveItem(int itemId, int ownerId);
        bool MoveItem(int itemId, int stockId, int ownerId);
        ItemState AddItemState(ItemState itemState, int itemId, int ownerId, bool addNew);
        IEnumerable<ItemStockHistory> GetItemHistory(int itemId, int ownerId);
        ItemState GetItemState(int stateId, int ownerId);
    }

    public class ItemsService : IItemsService
    {
        private readonly StocksDbContext _db;

        public ItemsService(StocksDbContext db)
        {
            _db = db;
        }

        public Item AddItem(Item item, int stockId, int ownerId)
        {
            if (item != null && _db.UsersStocks.FirstOrDefault(us => us.StockId == stockId && us.UserId == ownerId) != null)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                _db.ItemsStocksHistory.Add(new ItemStockHistory { ItemId = item.Id, StockId = stockId, ArrivalDate = DateTime.Now });
                _db.SaveChanges();
                item.ItemsStocksHistory = null;
                return item;
            }
            return null;
        }

        public Item GetItem(int itemId, int ownerId)
        {
            var itemStockHistory = GetItemLastEntry(itemId, ownerId);

            if (itemStockHistory == null)
                return null;

            var item = _db.Items.Find(itemId);
            item.ItemsStocksHistory = null;
            return item;
        }

        public IEnumerable<Item> GetStockItems(int stockId, int ownerId)
        {
            var userStock = _db.UsersStocks.FirstOrDefault(us => us.StockId == stockId && us.UserId == ownerId);

            if (userStock == null)
                return null;

            var items = _db.ItemsStocksHistory
                .Select(ish => ish.ItemId)
                .Distinct()
                .Select(id => _db.ItemsStocksHistory
                    .FirstOrDefault(ish1 => ish1.ItemId == id &&
                        ish1.ArrivalDate == _db.ItemsStocksHistory
                            .Where(ish2 => ish2.ItemId == ish1.ItemId)
                            .Max(ish2 => ish2.ArrivalDate))
                ).Distinct()
                .Where(ish => ish.StockId == stockId)
                .ToList()
                .Select(ish =>
                {
                    var item = _db.Items.Find(ish.ItemId);
                    item.ItemsStocksHistory = null;
                    item.ItemState = null;
                    return item;
                });

            return items;
        }

        public bool RemoveItem(int itemId, int ownerId)
        {
            var itemStockHistory = GetItemLastEntry(itemId, ownerId);

            if (itemStockHistory == null)
                return false;

            var item = _db.Items.Find(itemStockHistory.ItemId);

            if (item == null)
            {
                return false;
            }

            _db.Items.Remove(item);
            _db.SaveChanges();
            return true;
        }

        public bool MoveItem(int itemId, int stockId, int ownerId)
        {
            var itemStockHistory = GetItemLastEntry(itemId, ownerId);

            if (itemStockHistory == null)
                return false;

            _db.ItemsStocksHistory.Add(new ItemStockHistory { ItemId = itemId, StockId = stockId, ArrivalDate = DateTime.Now });
            _db.SaveChanges();
            return true;
        }

        public ItemState AddItemState(ItemState itemState, int itemId, int ownerId, bool addNew)
        {
            if (itemState == null)
                return null;

            var itemStockHistory = GetItemLastEntry(itemId, ownerId);

            if (itemStockHistory == null)
                return null;

            _db.ItemStates.Add(itemState);
            _db.SaveChanges();

            if (!addNew)
            {
                itemStockHistory.ItemStateId = itemState.Id;
            }
            else
            {
                var itemStockHistory1 = _db.ItemsStocksHistory.Add(new ItemStockHistory { ItemId = itemId, StockId = itemStockHistory.StockId, ArrivalDate = DateTime.Now });
                _db.SaveChanges();
                itemStockHistory1.Entity.ItemStateId = itemState.Id;
            }

            _db.SaveChanges();

            itemState.ItemStockHistory = null;
            return itemState;
        }

        public IEnumerable<ItemStockHistory> GetItemHistory(int itemId, int ownerId)
        {
            var itemStockHistory = GetItemLastEntry(itemId, ownerId);

            if (itemStockHistory == null)
                return null;

            var itemHistory = _db.ItemsStocksHistory
                .Where(ish => ish.ItemId == itemId)
                .OrderBy(ish => ish.ArrivalDate);
                //.Include(ish => ish.Item)
                //.Include(ish => ish.Stock)
                //.Include(ish => ish.ItemState)
                //.AsEnumerable()
                //.Select(ish =>
                //{
                //    ish.Item.ItemsStocksHistory = null;
                //    ish.Item.ItemState = null;
                //    ish.Stock.ItemsStocksHistory = null;
                //    ish.Stock.UsersStocks = null;
                //    ish.ItemState.ItemStockHistory = null;
                //    ish.Item = null;
                //    ish.Stock = null;
                //    ish.ItemState = null;
                //    return ish;
                //});

            //var itemHistory = _db.Items.Find(itemId)
            //    .ItemsStocksHistory
            //    .Select(ish =>
            //    {
            //        ish.Item = null;
            //        ish.Stock = null;
            //        ish.ItemState = null;
            //        return ish;
            //    });

            return itemHistory;
        }

        public ItemState GetItemState(int stateId, int ownerId)
        {
            var state = _db.ItemStates.Find(stateId);

            return state;
        }

        private ItemStockHistory GetItemLastEntry(int itemId, int ownerId)
        {
            var itemStockHistory = _db.ItemsStocksHistory
                .FirstOrDefault(ish => ish.ItemId == itemId &&
                    ish.ArrivalDate == _db.ItemsStocksHistory
                        .Where(ish1 => ish1.ItemId == itemId)
                        .Max(ish1 => ish1.ArrivalDate));

            if (itemStockHistory == null)
                return null;

            var userStock = _db.UsersStocks.FirstOrDefault(us => us.StockId == itemStockHistory.StockId && us.UserId == ownerId);

            if (userStock == null)
                return null;
            return itemStockHistory;
        }
    }
}
