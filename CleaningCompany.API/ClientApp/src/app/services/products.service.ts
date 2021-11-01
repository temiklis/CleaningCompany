import { Injectable } from "@angular/core";
import { ProductCard } from "../models/interfaces/Products/ProductCard";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class ProductsService {

  constructor(private httpService: HttpService) {

  }

  getProductCards(): Promise<ProductCard[]> {
    return this.httpService.GET<ProductCard[]>('Product/Cards');
  }
}
