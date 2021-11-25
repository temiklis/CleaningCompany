import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Material } from '../../models/interfaces/Materials/Material';
import { ProductCard } from '../../models/interfaces/Products/ProductCard';
import { HelperService } from '../../services/helper.service';
import { MaterialsService } from '../../services/materials.service';
import { ProductsService } from '../../services/products.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateOrderRequestUserInfoModalComponent } from '../modals/create-order-request-user-info-modal/create-order-request-user-info-modal.component';
import { OrderRequest } from '../../models/interfaces/OrderRequests/OrderRequest';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from '../../services/authorization/authorize.service';

@Component({
  selector: 'app-prices-services',
  templateUrl: './prices-services.component.html',
  styleUrls: ['./prices-services.component.scss']
})
export class PricesServicesComponent implements OnInit, OnDestroy {

  @ViewChild('ordering') ordering: ElementRef;
  products: ProductCard[] = [];

  value: number = 2;
  max: number = 3;

  selectedProducts: ProductCard[] = [];
  materials: Material[] = [];

  totalOrderPrice: number = 0;

  orderingVisible: boolean = false;

  userEmail: string;

  constructor(private productsService: ProductsService,
    private helperService: HelperService,
    private materialsService: MaterialsService,
    private modalService: NgbModal,
    private authorizeService: AuthorizeService) { }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.authorizeService.getUser().pipe(map(u => u && u.name)).subscribe(userEmail => {
      this.userEmail = userEmail;
    });

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
    let orderRequest: OrderRequest = {
      Address: null,
      Email: this.userEmail,
      FIO: null,
      Products: this.selectedProducts,
      RequestedDate: null
    }

    const modalRef = this.modalService.open(CreateOrderRequestUserInfoModalComponent, { backdrop: 'static', centered: true, size: 'l', });
    modalRef.componentInstance.orderRequest = orderRequest;
    modalRef.componentInstance.totalPrice = this.totalOrderPrice;

    modalRef.result.then(result => {
      if (result) {
        this.helperService.alert("Order Request was successfully created! We will contact you via E-mail. Also, you can check order request information in you profile.");
        this.selectedProducts = [];
        this.orderingVisible = false;
      }
    }).catch(error => {
      this.helperService.alertError(error);
    })
  }
}
