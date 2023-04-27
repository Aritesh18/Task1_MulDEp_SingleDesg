import { Designation } from './designation';
import { Department } from './department';
import { Employee } from './employee';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Register } from './register';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private httpClient: HttpClient) {}
  getAllEmployees(): Observable<Employee[]> {
    return this.httpClient.get<Employee[]>(
      'https://localhost:44386/api/employee'
    );
  }
  // saveEmployee(newEmployee: Employee): Observable<Employee> {
  //   return this.httpClient.post<Employee>(
  //     'https://localhost:44386/api/employee',
  //     newEmployee
  //   );
  // }
  saveEmployee(employee: Employee): Observable<any> {
    debugger;
    return this.httpClient.post<Employee>(
      'https://localhost:44386/api/employee',
      employee
    );
  }
  RegisterEmployee(register:Register):Observable<any>{
    return this.httpClient.post<Register>('https://localhost:44386/api/user/Register',register
    );

  }
//   updateEmployee(editEmployee: Employee): Observable<Employee> {
//     return this.httpClient.put<Employee>(
//       'https://localhost:44386/api/employee',
     
//  editEmployee
//     );
//   }
updateEmployee(editEmployee: Employee): Observable<any> {
  // debugger;
  return this.httpClient.put<Employee>(
    'https://localhost:44386/api/employee',
    editEmployee
  );
}

  deleteEmployee(id: number): Observable<any> {
    return this.httpClient.delete<any>(
      'https://localhost:44386/api/employee/' + id
    );
  }
  getDepartment(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44386/api/department');
  }
  getDesignation(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44386/api/designation');
  }
  
}
