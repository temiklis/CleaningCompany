import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialSearchParams } from '../../../models/interfaces/Materials/MaterialSearchParams';
import { MaterialWithProducts } from '../../../models/interfaces/Materials/MaterialWithProducts';
import { XPagination } from '../../../models/interfaces/X-Pagination';
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

  pagination: XPagination;

  constructor(private materialService: MaterialsService,
    private router: Router) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getMaterials();
  }

  getMaterials() {
    this.materialService.getAllMaterials(this.searchParams).then(results => {
      this.materials = results;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToCreateMaterial() {
    this.router.navigate(['admin/materials/create']);
  }

  goToMaterialDetails(id: number) {
    this.router.navigate([`admin/materials/${id}`]);
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
