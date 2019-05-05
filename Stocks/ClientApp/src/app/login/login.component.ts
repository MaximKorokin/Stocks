import { Component, Inject, OnInit } from '@angular/core';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  name: string;
  password: string;

  url: string;

  constructor(private manager: GlobalUserManager, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Users/";
  }

  ngOnInit() {
  }

  login() {
    console.log("login btn");
    if (this.name === "" || this.password === "") {
      return;
    }

    this.manager.login(this.url + "authenticate", <User>{ name: this.name, password: this.password }, (user) => { console.log(user); });
  }
}
