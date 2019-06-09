import { Component } from '@angular/core';
import { User, GlobalUserManager } from '../global-user-manager';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
})
export class CreateUserComponent {
  user: User;
  repeatPassword: string;

  constructor(private router: Router, public manager: GlobalUserManager) {
    this.user = <User>{};
  }

  createUser() {
    if (this.user.password != this.repeatPassword) {
      alert("Passwords mismatch");
      return;
    }
    this.manager.post("users/register", this.user, user => {
      this.router.navigate(["./adm-users"]);
    });
  }
}
