using Microsoft.EntityFrameworkCore;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Data
{
    public class StocksDbContext : DbContext
    {
        public StocksDbContext(DbContextOptions<StocksDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<UserStock> UsersStocks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemState> ItemStates { get; set; }
        public DbSet<ItemStockHistory> ItemsStocksHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<UserStock>(entity =>
            {
                entity.HasKey(us => new { us.UserId, us.StockId });

                entity.HasOne(us => us.User)
                .WithMany(u => u.UsersStocks)
                .HasForeignKey(us => us.UserId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => i.Id);
            });

            modelBuilder.Entity<ItemState>(entity =>
            {
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<ItemStockHistory>(entity =>
            {
                entity.HasKey(ish => new { ish.ItemId, ish.StockId, ish.ItemStateId });

                entity.HasOne(ish => ish.Item)
                .WithMany(i => i.ItemsStocksHistory)
                .HasForeignKey(ish => ish.ItemId)
                .HasPrincipalKey(i => i.Id);

                entity.HasOne(ish => ish.Stock)
                .WithMany(s => s.ItemsStocksHistory)
                .HasForeignKey(ish => ish.StockId)
                .HasPrincipalKey(s => s.Id);

                entity.HasOne(ish => ish.ItemState)
                .WithOne(s => s.ItemStockHistory)
                .HasForeignKey<ItemStockHistory>(ish => ish.ItemStateId)
                .HasPrincipalKey<ItemState>(s => s.Id);
            });
        }
    }
}
