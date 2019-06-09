import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-about',
  templateUrl: './stock-items.component.html',
})
export class StockItemsComponent {
  public items: Item[];
  private stockId;

  constructor(private route: ActivatedRoute, private router: Router, private manager: GlobalUserManager) {
    this.stockId = this.route.snapshot.paramMap.get('id');

    this.manager.get("items/" + this.stockId, (items) => {
      this.items = items;
    });
  }

  addItem() {
    this.router.navigate(["./add-item/" + this.stockId]);
  }

  moveItem(event) {
    console.log("./move-item/" + event.target.name);
    this.router.navigate(["./move-item/" + event.target.name]);
  }
}

export interface Item {
  id: number;
  name: string;
  capacity: number;
}
