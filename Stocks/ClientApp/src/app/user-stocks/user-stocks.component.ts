import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalUserManager } from '../global-user-manager';

@Component({
  selector: 'app-user-stocks',
  templateUrl: './user-stocks.component.html',
})
export class UserStocksComponent {
  stocks: Stock[];

  constructor(private router: Router, private manager: GlobalUserManager) {
    manager.get("Stocks", s => {
      this.stocks = s;
    });
  }

  addStock() {
    this.router.navigate(["./add-stock"]);
  }
}

export interface Stock {
  id: number;
  name: string;
  capacity: number;
}
