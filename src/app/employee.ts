export class Employee {
  employeeId: number;
  employeeName: string;
  employeeAddress: string;
  employeeSalary: number;
 // departmentEmployees: any;
  departmentId: any[];
  departmentName: any;
  designationId: number;
  designationName: any;
 // department: any;
 // designation: any;
  constructor() { 
    this.employeeId = 0;
    this.employeeName = '';
    this.employeeAddress = '';
    this.employeeSalary = 0;
  //  this.departmentEmployees = null;
    this.designationId = 0;
    this.designationName = null;
    this.departmentName = null;
    this.departmentId = [];
  //  this.department = null;
   // this.designation = null;
  }
}
