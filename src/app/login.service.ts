import { JwtHelperService } from '@auth0/angular-jwt';
import { Login } from './login';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  CurrentUserName: any = '';
  constructor(private httpClient: HttpClient, private router: Router, private jwtHelperService:JwtHelperService) {}
  CheckUser(login: Login): Observable<any> {
    // Sends a POST request to the backend API endpoint with the login credentials
    return this.httpClient
      .post<any>('https://localhost:44386/api/account/authenticate', login)
      .pipe(
        // Uses the `map` operator to transform the response data
        map((u) => {
          // Checks if the response data is not null or undefined
          if (u) {
            // If the response data is not null, sets the `CurrentUserName` property to the user's username
            this.CurrentUserName = u.userName;
            // Stores the user information in the `sessionStorage` object as a stringified JSON object
            sessionStorage['currentUser'] = JSON.stringify(u);
          }
          // Returns the transformed response data
          return u;
        })
      );
  }
  
  LogOut() {
    this.CurrentUserName = '';
    sessionStorage.removeItem('currentUser');
    this.router.navigateByUrl('/login');
  }
  public isAuthenticated():boolean // to check JSON web token is expired or not
  {
    if(this.jwtHelperService.isTokenExpired()) // user is not authenticated and token is expired then comes false
    {
      return false;
    }
    else // when user is authenticated then comes true
    {
      return true;
    }
  }
}
