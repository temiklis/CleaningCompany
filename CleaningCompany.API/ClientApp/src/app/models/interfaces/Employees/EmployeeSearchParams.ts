import { PaginationParams } from "../PaginationParams";

export interface EmployeeSearchParams extends PaginationParams {
  Email: string;
  Name: string;
}
