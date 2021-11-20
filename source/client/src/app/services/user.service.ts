import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseURL: string;

  cartItemcount$ = new Subject<any>();

  constructor(private http: HttpClient) {
    this.baseURL = this.getBaseUrl();
  }

  registerUser(userdetails) {
    return this.http.post(this.baseURL + 'account/new-account', userdetails)
      .pipe(map(response => {
        return response;
      }));
  }

  public getBaseUrl(): string {
    return environment.baseUrl;
  }
}
