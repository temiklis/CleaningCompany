import { Injectable } from "@angular/core";
import { Material } from "../models/interfaces/Materials/Material";
import { MaterialIdName } from "../models/interfaces/Materials/MaterialIdName";
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

  getAllIdNameMaterials(): Promise<MaterialIdName[]> {
    return this.httpService.GET<MaterialIdName[]>(`Material/All`);
  }

  getMaterialById(id: number): Promise<Material> {
    return this.httpService.GET<Material>(`Material/${id}`);
  }

  createMaterial(material: Material): Promise<any> {
    return this.httpService.POST('Material', material);
  }

  updateMaterial(material: Material): Promise<any> {
    return this.httpService.PUT('Material', material);
  }

  deleteMaterial(id: number): Promise<any> {
    return this.httpService.DELETE(`Material/${id}`);
  }
}
