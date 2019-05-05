import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-about',
  templateUrl: './stock-items.component.html',
})
export class StockItemsComponent {
  private items: Item[];

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("items/" + id, (items) => {
      this.items = items;
    });
  }
}

export interface Item {
  id: number;
  name: string;
  capacity: number;
}
