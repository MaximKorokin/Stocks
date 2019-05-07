import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  itemId;

  constructor(private route: ActivatedRoute, private router: Router, private manager: GlobalUserManager) {
    this.itemId = this.route.snapshot.paramMap.get('id');

    this.manager.get("items/history/" + this.itemId, items => {
      this.itemsStocksHistory = items;

      this.manager.get("stocks", stocks => {
        this.stocks = stocks;

        for (let item of this.itemsStocksHistory) {
          item.stock = this.stocks.find(s => s.id == item.stockId);
        }
      });
    });

    this.manager.get("items/get/" + this.itemId, item => {
      this.item = item;
    });
  }

  addState() {
    this.router.navigate(["./add-state/" + this.itemId]);
  }
}

export interface ItemStockHistory {
  itemId: number,
  stockId: number,
  arrivalDate: Date,
  itemStateId?: number,

  stock: Stock
}
