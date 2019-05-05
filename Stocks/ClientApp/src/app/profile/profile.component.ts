import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class LoginComponent implements OnInit {
  url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private manager: GlobalUserManager) {
    this.url = baseUrl + "api/Users/";
  }

  ngOnInit() {
  }

}
