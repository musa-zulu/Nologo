import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { SubscriptionService } from './subscription.service';
import { UserService } from './user.service';
import { environment } from 'src/environments/environment';
import { ServerConfig } from './server-config';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  oldUserId;
  baseURL: string;

  constructor(
    private http: HttpClient,
    private userService: UserService,
    private subscriptionService: SubscriptionService,
    private _serverConfig: ServerConfig) {
      this.baseURL = this._serverConfig.getBaseUrl() + 'api/';
     }

  login(user) {
    return this.http.post<any>(this.baseURL + 'account/login', user)
      .pipe(map(response => {
        if (response && response.accessToken) {
          this.oldUserId = localStorage.getItem('userId');
          localStorage.setItem('authToken', response.accessToken);
          this.setUserDetails();

          var userId = localStorage.getItem('userId');
          localStorage.setItem('userId', userId);
        }
        return response;
      }));
  }

  setUserDetails() {
    if (localStorage.getItem('authToken')) {
      const userDetails = new User();
      const decodeUserDetails = JSON.parse(atob(localStorage.getItem('authToken').split('.')[1]));
      
      userDetails.userId = decodeUserDetails.userid;
      userDetails.email = decodeUserDetails.sub;
      userDetails.role = Number(decodeUserDetails.role);
      userDetails.isLoggedIn = true;

      this.subscriptionService.userData.next(userDetails);
    }
  }

  logout() {
    localStorage.clear();
    this.subscriptionService.userData.next(new User());
    this.userService.cartItemcount$.next(0);
    this.setTempUserId();
  }

  setTempUserId() {
    if (!localStorage.getItem('userId')) {
      const tempUserID = this.generateTempUserId();
      localStorage.setItem('userId', tempUserID.toString());
    }
  }

  generateTempUserId() {
    return Math.floor(Math.random() * (99999 - 11111 + 1) + 12345);
  }
}
