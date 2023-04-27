import { EmployeeService } from './../employee.service';
import { Component } from '@angular/core';
import { Designation } from '../designation';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.scss']
})
export class DesignationComponent {
  designationList:Designation[]=[];

  constructor(private EmployeeService:EmployeeService){}
  ngOnInit()
  {
    this.getDesignation();
  }
  getDesignation()
  {
    this.EmployeeService.getDesignation().subscribe(
      (response)=>{
        this.designationList=response;
      },
      (Error)=>{
        console.log(Error);
      }
    )
  }
}
