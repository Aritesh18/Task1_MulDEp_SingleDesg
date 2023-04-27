import { LoginService } from './../login.service';
import { Component } from '@angular/core';
import { Login } from '../login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  login: Login = new Login();
  loginErrorMsg: string = '';
  constructor(private loginService: LoginService, private router: Router) {}

  LoginClick() {
    // alert(this.login.userName)
    this.loginService.CheckUser(this.login).subscribe(
      (response) => {
        // this.router.navigateByUrl("./employee");
        this.router.navigateByUrl('/employee');
      },
      (error) => {
        console.log(error);
        //alert('wrong user/password');
        this.loginErrorMsg = 'Login Failure';
      }
    );
  }
}
