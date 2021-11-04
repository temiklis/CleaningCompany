import { Injectable } from "@angular/core";
import { Material } from "../models/interfaces/Materials/Material";
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
}
