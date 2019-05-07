import { Component } from '@angular/core';
import { User, GlobalUserManager } from '../global-user-manager';
import { Router } from '@angular/router';

@Component({
  selector: 'adm-users',
  templateUrl: './adm-users.component.html',
})
export class AdmUsersComponent {
  users: User[];

  constructor(private router: Router, private manager: GlobalUserManager) {
    manager.get("users", users => {
      const userId = this.manager.getUser().id;
      for (let i = 0; i < users.length; i++) {
        if (users[i].id === userId) {
          users.splice(i, 1);
          break;
        }
      }
      this.users = users;
    });
  }
}
