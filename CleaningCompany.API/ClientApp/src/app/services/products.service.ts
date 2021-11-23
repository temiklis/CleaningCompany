import { Injectable } from "@angular/core";
import { Product } from "../models/interfaces/Products/Product";
import { ProductCard } from "../models/interfaces/Products/ProductCard";
import { ProductSearchParams } from "../models/interfaces/Products/ProductSearchParams";
import { HelperService } from "./helper.service";
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

  getAllProducts(parameters: ProductSearchParams): Promise<Array<Product>> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<Product[]>('Product', validParameters);
  }
}
