import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Material } from '../../models/interfaces/Materials/Material';
import { ProductCard } from '../../models/interfaces/Products/ProductCard';
import { HelperService } from '../../services/helper.service';
import { MaterialsService } from '../../services/materials.service';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-prices-services',
  templateUrl: './prices-services.component.html',
  styleUrls: ['./prices-services.component.scss']
})
export class PricesServicesComponent implements OnInit {
  @ViewChild('ordering') ordering: ElementRef;
  products: ProductCard[] = [];

  value: number = 2;
  max: number = 3;

  selectedProducts: ProductCard[] = [];
  materials: Material[] = [];

  totalOrderPrice: number = 0;

  orderingVisible: boolean = false;

  constructor(private productsService: ProductsService,
    private helperService: HelperService,
    private materialsService: MaterialsService) { }

  ngOnInit(): void {
    this.getProductCards();
  }

  getProductCards() {
    this.productsService.getProductCards().then(result => {
      this.products = result;
    }).catch(err => {
      this.helperService.alertError(err.error);
    })
  }

  getMaterials(product: ProductCard) {
    this.materialsService.getMaterialsForProduct(product.Id).then(result => {
      this.materials.push(...result);
      product.Materials = result;
      this.selectedProducts.push(product);

      this.calculateTotalOrderPrice();
      this.ordering.nativeElement.scrollIntoView();
    }).catch(err => {
      this.helperService.alertError(err.error);
    })
  }

  selectProduct(product: ProductCard) {
    this.orderingVisible = true;
    this.getMaterials(product);
  }

  removeProduct(product: ProductCard, index: number) {
    product.Materials.forEach(material => {
      let materialIndex = this.materials.findIndex(m => m.Id == material.Id);
      this.materials.splice(materialIndex, 1);
    })

    this.selectedProducts.splice(index, 1);

    this.calculateTotalOrderPrice();
  }

  getUniqueMaterials(): Material[] {
    return [...new Set(this.materials)];
  }

  getProductAmount(id: number): number {
    return this.selectedProducts.filter(p => p.Id == id).length;
  }

  calculateTotalOrderPrice() {
    let productPrice = this.selectedProducts.map(p => p.BasePrice).reduce((a, b) => a + b, 0) as number;
    let materialsPrice = this.materials.map(p => p.Price).reduce((a, b) => a + b, 0) as number;
    this.totalOrderPrice = Math.round((productPrice + materialsPrice) * 100) / 100;

    if (this.totalOrderPrice == 0) {
      this.orderingVisible = false;
    }
  }

  createOrder() {

  }
}
