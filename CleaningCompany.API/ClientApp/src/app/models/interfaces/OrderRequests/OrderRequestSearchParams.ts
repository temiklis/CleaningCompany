import { PaginationParams } from "../PaginationParams";

export interface OrderRequestSearchParams extends PaginationParams {
  FIO: string;
  Email: string;
  Address: string;
}
