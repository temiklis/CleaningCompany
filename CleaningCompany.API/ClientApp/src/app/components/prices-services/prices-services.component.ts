import { Component, OnInit } from '@angular/core';
import { ProductCard } from '../../models/interfaces/Products/ProductCard';
import { HelperService } from '../../services/helper.service';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-prices-services',
  templateUrl: './prices-services.component.html',
  styleUrls: ['./prices-services.component.scss']
})
export class PricesServicesComponent implements OnInit {

  products: ProductCard[] = [];
  constructor(private productsService: ProductsService,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.getProductCards();
  }

  getProductCards() {
    this.productsService.getProductCards().then(result => {
      debugger;
      this.products = result;
    }).catch(err => {
      this.helperService.alertError(err.error);
    })
  }

}
