import { PaginationParams } from "../PaginationParams";

export interface ClientSearchParams extends PaginationParams {
  Name: string;
  Email: string;
}
