import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../models/interfaces/Employees/Employee';
import { EmployeeSearchParams } from '../../../models/interfaces/Employees/EmployeeSearchParams';
import { XPagination } from '../../../models/interfaces/X-Pagination';
import { EmployeeService } from '../../../services/employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  employees: Employee[];

  searchParams: EmployeeSearchParams;
  defaultSearchParams: EmployeeSearchParams = {
    Name: null,
    Email: null,
    PageNumber: 1,
    PageSize: 10
  }

  pagination: XPagination;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getEmployees();
  }

  getEmployees() {
    this.employeeService.getAllEmployees(this.searchParams).then(employees => {
      this.employees = employees;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToEmployeeDetails(id: number) {

  }

  goToCreateEmployee() {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getEmployees();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getEmployees();
  }

}
