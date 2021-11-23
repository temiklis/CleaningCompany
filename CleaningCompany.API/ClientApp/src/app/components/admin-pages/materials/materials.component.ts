import { Component, OnInit } from '@angular/core';
import { MaterialSearchParams } from '../../../models/interfaces/Materials/MaterialSearchParams';
import { MaterialWithProducts } from '../../../models/interfaces/Materials/MaterialWithProducts';
import { MaterialsService } from '../../../services/materials.service';

@Component({
  selector: 'app-materials',
  templateUrl: './materials.component.html',
  styleUrls: ['./materials.component.scss']
})
export class MaterialsComponent implements OnInit {

  materials: MaterialWithProducts[];

  searchParams: MaterialSearchParams;
  defaultSearchParams: MaterialSearchParams = {
    Name: null,
    PageNumber: 1,
    PageSize: 10
  }

  constructor(private materialService: MaterialsService) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getMaterials();
  }

  getMaterials() {
    this.materialService.getAllMaterials(this.searchParams).then(results => {
      this.materials = results;
    })
  }

  goToCreateMaterial() {

  }

  goToMaterialDetails(id: number) {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getMaterials();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getMaterials();
  }

}
