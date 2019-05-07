import { Component } from '@angular/core';
import { Item } from '../stock-items/stock-items.component';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
})
export class AddItemComponent {
  private item: Item;
  private stockId;

  constructor(private route: ActivatedRoute, private router: Router, private manager: GlobalUserManager) {
    this.stockId = this.route.snapshot.paramMap.get('id');
    this.item = <Item>{};
  }

  addItem() {
    this.manager.post("items/add/" + this.stockId, this.item, item => {
      this.router.navigate(["./item-history/" + item.id]);
    });
  }
}
