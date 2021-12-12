import { Injectable } from "@angular/core";
import { Product } from "../models/interfaces/Products/Product";
import { ProductCard } from "../models/interfaces/Products/ProductCard";
import { ProductSearchParams } from "../models/interfaces/Products/ProductSearchParams";
import { ProductWithMaterials } from "../models/interfaces/Products/ProductWithMaterials";
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

  getProductById(id: number): Promise<ProductWithMaterials> {
    return this.httpService.GET<ProductWithMaterials>(`Product/${id}`);
  }

  updateProduct(product: ProductWithMaterials): Promise<any> {
    return this.httpService.PUT(`Product`, product);
  }

  createProduct(product: ProductWithMaterials): Promise<any> {
    return this.httpService.POST('Product', product);
  }

  deleteProduct(id: number): Promise<any> {
    return this.httpService.DELETE(`Product/${id}`);
  }
}
