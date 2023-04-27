import { JwtHelperService } from '@auth0/angular-jwt';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtactiveguardService implements CanActivate {

  constructor(private loginService:LoginService, private router:Router, private jwtHelperService:JwtHelperService) { }
  canActivate(route:ActivatedRouteSnapshot): boolean
  {
    if(this.loginService.isAuthenticated())
    {
      return true;
    }
    else
    {
      alert('You are not Authorized to Access this Page');
      this.router.navigateByUrl("/login");
      return false;
    }
  }
}
