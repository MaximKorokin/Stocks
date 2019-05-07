import { Component } from '@angular/core';
import { Stock } from '../user-stocks/user-stocks.component';
import { Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-add-stock',
  templateUrl: './add-stock.component.html',
})
export class AddStockComponent {
  stock: Stock;

  constructor(private router: Router, private manager: GlobalUserManager) {
    this.stock = <Stock>{};
  }

  addStock() {
    this.manager.post("stocks/add", this.stock, stock => {
      this.router.navigate(["./stock-items/" + stock.id]);
    });
  }
}
