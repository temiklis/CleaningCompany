import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../../../models/interfaces/Products/Product';
import { ProductSearchParams } from '../../../models/interfaces/Products/ProductSearchParams';
import { XPagination } from '../../../models/interfaces/X-Pagination';
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

  pagination: XPagination;

  constructor(private productService: ProductsService,
    private router: Router) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getProducts();
  }

  getProducts() {
    this.productService.getAllProducts(this.searchParams).then(results => {
      this.products = results;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToCreateProduct() {
    this.router.navigate(['admin/products/create']);
  }

  goToProductDetails(id: number) {
    this.router.navigate([`admin/products/${id}`]);
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
