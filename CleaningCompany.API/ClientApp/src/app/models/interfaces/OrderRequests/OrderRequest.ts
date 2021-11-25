import { ProductCard } from "../Products/ProductCard";

export interface OrderRequest {
  Email: string;
  FIO: string;
  Address: string;
  Products: ProductCard[];
  RequestedDate: Date;
}
