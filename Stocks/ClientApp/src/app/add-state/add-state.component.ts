import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-add-state',
  templateUrl: './add-state.component.html',
})
export class AddStateComponent {
  state: ItemState;
  itemId;
  addNewState: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private manager: GlobalUserManager) {
    this.itemId = this.route.snapshot.paramMap.get('id');
    this.state = <ItemState>{};
  }

  addState() {
    this.manager.post("items/" + (this.addNewState ? "add" : "write") + "state/" + this.itemId, this.state, state => {
      this.router.navigate(["./item-history/" + this.itemId]);
    });
  }
}

export interface ItemState {
  id: number;
  mass: number;
}
