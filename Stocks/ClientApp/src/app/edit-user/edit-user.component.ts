import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user: User;
  oldPassword: string;
  newPassword1: string;
  newPassword2: string;

  constructor(private router: Router, private manager: GlobalUserManager) {
    this.user = { ...this.manager.getUser() };
    if (this.user === null || this.user === undefined)
      this.router.navigate(["."]);
  }

  ngOnInit() {
  }

  edit() {
    console.log(this.user.password);
    if (this.manager.hash(this.oldPassword) === this.user.password && this.newPassword1 === this.newPassword2) {
      this.user.password = this.newPassword1;
    }
    this.manager.post("Users/edit", this.user, u => {
      this.manager.setUser(this.user);

      this.router.navigate(["./profile"]);
    });
  }

}
