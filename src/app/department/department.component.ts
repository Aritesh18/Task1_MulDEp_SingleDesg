import { EmployeeService } from './../employee.service';
import { Component } from '@angular/core';
import { Department } from '../department';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent {
  departmentList:Department[]=[];

  constructor(private EmployeeService:EmployeeService){}
  ngOnInit()
  {
    this.getDepartment();
  }
  getDepartment()
  {
    this.EmployeeService.getDepartment().subscribe(
      (response)=>{
        this.departmentList=response;
      },
      (Error)=>{
        console.log(Error);
      }
    )
  }
}
