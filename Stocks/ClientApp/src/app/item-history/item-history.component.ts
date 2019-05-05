import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';
import { Item } from '../stock-items/stock-items.component';
import { Stock } from '../user-stocks/user-stocks.component';

@Component({
  selector: 'app-about',
  templateUrl: './item-history.component.html',
})
export class ItemHistoryComponent {
  itemsStocksHistory: ItemStockHistory[];
  stocks: Stock[];
  item: Item;

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("items/history/" + id, items => {
      console.log(items);
      this.itemsStocksHistory = items;

      this.manager.get("stocks", stocks => {
        this.stocks = stocks;

        for (let item of this.itemsStocksHistory) {
          item.stock = this.stocks.find(s => s.id == item.stockId);
        }
      });
    });

    this.manager.get("items/get/" + id, item => {
      this.item = item;
    });
  }
}

export interface ItemStockHistory {
  itemId: number,
  stockId: number,
  arrivalDate: Date,
  itemStateId?: number,

  stock: Stock
}
