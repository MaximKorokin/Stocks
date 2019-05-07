import { Component } from '@angular/core';
import { ItemState } from '../add-state/add-state.component';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-about',
  templateUrl: './item-state.component.html',
})
export class ItemStateComponent {
  state: ItemState;
  stateId;

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) {
    this.stateId = this.route.snapshot.paramMap.get('id');

    manager.get("items/getstate/" + this.stateId, state => {
      this.state = state;
    });
  }
}
