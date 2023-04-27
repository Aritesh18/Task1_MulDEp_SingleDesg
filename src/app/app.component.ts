import { Component } from '@angular/core';
import { LoginService } from './login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebApifor_emp_dep_deg';
  constructor(public loginService:LoginService){}
  logOutClick()
  {
    this.loginService.LogOut();
  }
} 