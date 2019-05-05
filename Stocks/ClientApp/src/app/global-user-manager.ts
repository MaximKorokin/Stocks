import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class GlobalUserManager {
  //public id: number;
  //public name: string;
  //private password: string;
  //private role: string;
  //private language: string;

  public user: User;

  private locale: Object;

  constructor(private http: HttpClient) {
    this.user = JSON.parse(localStorage.getItem("currentUser"));
    console.log(this.user);
  }

  setUser(user: User) {
    if (this.user !== null && user !== null && this.user.language !== user.language) {
      //this.http.get("./assets/locale/" + user.language + ".json").subscribe(data => {
      //  this.locale = data;
      //  console.log(this.locale);
      //});
    }

    //this.id = user.id;
    //this.name = user.name;
    //this.password = this.hash(user.password);
    //this.role = user.role;
    //this.language = user.language;

    localStorage.setItem("currentUser", JSON.stringify(user));
  }
  getUser() {
    return this.user;
  }
  isLoggedIn() {
    return this.user !== null;
  }

  login(path: string, user: User, callback: Function) {
    this.http.post<User>(path, { name: user.name, password: user.password }).subscribe(result => {
      callback(result);
      this.setUser(result);
    }, error => console.error(error));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }

  get(path: string, callback: Function) {
    this.http.get(location.origin + "/api/" + path).subscribe(
      obj => {
        callback(obj);
      }, error => console.error(error));
  }

  post(path: string, obj: object, callback: Function) {
    if (!this.isLoggedIn())
      return;

    this.http.post<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        if (callback != null)
          callback(obj);
      }, error => console.error(error));
  }

  hash(str: string) {
    return str;
  }

  translate(key: string) {
    return this.locale[key];
  }
}

export interface User {
  id: number;
  name: string;
  password: string;
  role: string;
  language: string;
  token: string;
}
