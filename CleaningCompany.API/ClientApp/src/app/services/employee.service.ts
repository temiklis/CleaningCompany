import { Injectable } from "@angular/core";
import { Employee } from "../models/interfaces/Employees/Employee";
import { EmployeeSearchParams } from "../models/interfaces/Employees/EmployeeSearchParams";
import { IdleEmployee } from "../models/interfaces/Employees/IdleEmployee";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class EmployeeService {

  constructor(private httpService: HttpService) {

  }

  getAllEmployees(parameters: EmployeeSearchParams): Promise<Employee[]> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<Employee[]>(`Employee`, validParameters);
  }

  getIdleEmployees(date: Date): Promise<IdleEmployee[]> {
    return this.httpService.GET<IdleEmployee[]>(`Employee/Idle?date=${date}`);
  }
}
