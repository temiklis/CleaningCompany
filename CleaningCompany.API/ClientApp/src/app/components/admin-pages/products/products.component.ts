import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../../../models/interfaces/Products/Product';
import { ProductSearchParams } from '../../../models/interfaces/Products/ProductSearchParams';
import { ProductsService } from '../../../services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: Product[];

  searchParams: ProductSearchParams;
  defaultSearchParams: ProductSearchParams = {
    Name: null,
    PageNumber: 1,
    PageSize: 10
  }

  constructor(private productService: ProductsService,
    private router: Router) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getProducts();
  }

  getProducts() {
    this.productService.getAllProducts(this.searchParams).then(results => {
      this.products = results;
    })
  }

  goToCreateProduct() {

  }

  goToProductDetails(id: number) {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getProducts();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getProducts();
  }

}
