import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-move-item',
  templateUrl: './move-item.component.html',
})
export class MoveItemComponent {
  stockId;
  itemId;

  constructor(private route: ActivatedRoute, private router: Router, private manager: GlobalUserManager) {
    this.itemId = this.route.snapshot.paramMap.get('id');
  }

  moveItem() {
    this.manager.post("items/move/" + this.itemId + "/" + this.stockId, null, result => {
      this.router.navigate(["./stock-items/" + this.stockId]);
    });
  }
}
