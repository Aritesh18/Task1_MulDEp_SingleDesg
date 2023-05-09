import { Register } from './../register';
import { EmployeeService } from './../employee.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  register:Register=new Register();
  constructor(private employeeService:EmployeeService){}
  RegisterClick()
  {
    this.employeeService.RegisterEmployee(this.register).subscribe(
      (Response)=>{
        this.register.employeeName="";
        this.register.registerEmail="";
        this.register.registerPassword="";
      },
      (Error)=>{
        console.log(Error);
        // this.registerErrorMsg= "The password do not match."
      }
    );
  }

}