import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;

  constructor(private manager: GlobalUserManager, private router: Router) {
    this.user = manager.getUser();
  }

  ngOnInit() {
  }

  logout() {
    this.manager.logout();
    this.router.navigate(["./login"]);
  }

  edit() {
    this.router.navigate(["./edit-user"]);
  }
}
