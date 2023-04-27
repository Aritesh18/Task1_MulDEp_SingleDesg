import { Designation } from './../designation';
import { EmployeeService } from './../employee.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from '../employee';
import { Department } from '../department';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html', 
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent implements OnInit {
  employeeList: Employee[] = [];
  departmentList: Department[]= [];
  designationList: Designation[] = [];
  newEmployee: Employee = new Employee();
  editEmployee: Employee = new Employee();
  dropdownSettings: IDropdownSettings = {
    
  };

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.getAll();
    this.getDep();
    this.getDsg();
    this.departmentList =[];
    this.dropdownSettings = {
      idField: 'departmentId',
      textField: 'departmentName',
      allowSearchFilter: true
    };
  }
  getAll() {
    this.employeeService.getAllEmployees().subscribe(
      (response) => {
        this.employeeList = response;
        //console.log(this.employeeList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getDep() {
    this.employeeService.getDepartment().subscribe(
      (response) => {
        this.departmentList = response;
        //console.log(this.departmentList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getDsg() {
    this.employeeService.getDesignation().subscribe(
      (response) => {
        this.designationList = response; 
        // console.log(this.designationList);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  SaveClick() {
// debugger
// alert(this.newEmployee.employeeName)
// alert(this.newEmployee.employeeAddress)
// alert(this.newEmployee.employeeSalary)
// alert(this.newEmployee.departmentId)
// alert(this.newEmployee.designationId)
debugger;
// var getDeptId=this.newEmployee.departmentId.map(x=>x.departmentId);
// this.newEmployee.departmentId=[]
// this.newEmployee.departmentId=getDeptId;
     this.employeeService.saveEmployee(this.newEmployee).subscribe(
       (response) => {
        // debugger
         console.log(this.newEmployee);
        
          this.getAll();
        // this.getDep();
        // this.getDsg();
       },
       (error) => {
         console.log(error);
       }
     );
   }
   EditClick(emp:Employee){
 this.editEmployee=emp;  
   }
   UpdateClick(){
    // alert(this.editEmployee.employeeName)
    // alert(this.editEmployee.employeeAddress)
    // alert(this.editEmployee.employeeSalary)
    // alert(this.editEmployee.departmentId)
    // alert(this.editEmployee.designationId)
    this.employeeService.updateEmployee(this.editEmployee).subscribe(
      (response)=>{
    // debugger
    console.log(this.editEmployee);
          this.getAll();
      },
      (error)=>{
        console.log(error);
      }
      )
  }
   DeleteClick(id: number) {
    this.employeeService.deleteEmployee(id).subscribe(
      (result) => {
        this.getAll();
      },

      (error) => {
        console.log(error);
      }
    );
  }
}
