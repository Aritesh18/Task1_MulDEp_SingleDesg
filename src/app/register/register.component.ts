import { EmployeeService } from './../employee.service';
import { Component } from '@angular/core';
import { Register } from '../register';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  register:Register=new Register();
  constructor(private employeeService:EmployeeService,private router:Router){}
  RegisterClick()
  {
    alert(this.register.UserName)
    this.employeeService.RegisterEmployee(this.register).subscribe(
      (response)=>{
        this.router.navigateByUrl('/register');
       },
      (error)=>{
        console.log(error);
        alert('wrong user/password');
      }
      );
  }

}
