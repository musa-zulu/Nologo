import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseURL: string;

  cartItemcount$ = new Subject<any>();

  constructor(private http: HttpClient) {
    this.baseURL = '/api/accounts/';
  }

  registerUser(userdetails) {
    return this.http.post(this.baseURL, userdetails)
      .pipe(map(response => {
        return response;
      }));
  }

  validateUserName(userName: string) {
    return this.http.get(this.baseURL + 'validateUserName/' + userName)
      .pipe(map(response => {
        return response;
      }));
  }

  validateEmail(email: string) {
    return this.http.get(this.baseURL + 'validateEmail/' + email)
    .pipe(map(response => {
      return response;
    }));
  }
}
