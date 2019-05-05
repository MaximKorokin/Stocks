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
  items: Item[];
  stocks: Stock[];

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("items/" + id, (items) => {
      this.items = items;
    });
  }
}
