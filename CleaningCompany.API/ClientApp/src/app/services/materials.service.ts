import { Injectable } from "@angular/core";
import { Material } from "../models/interfaces/Materials/Material";
import { MaterialSearchParams } from "../models/interfaces/Materials/MaterialSearchParams";
import { MaterialWithProducts } from "../models/interfaces/Materials/MaterialWithProducts";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class MaterialsService {
  constructor(private httpService: HttpService) {

  }

  getMaterialsForProduct(id: number): Promise<Material[]> {
    return this.httpService.GET<Material[]>(`Material/ProductMaterials/${id}`);
  }

  getAllMaterials(parameters: MaterialSearchParams): Promise<MaterialWithProducts[]> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<MaterialWithProducts[]>('Material', validParameters)
  }
}
