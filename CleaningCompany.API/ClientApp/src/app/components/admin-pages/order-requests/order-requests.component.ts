import { Component, OnInit } from '@angular/core';
import { OrderRequestSearchParams } from '../../../models/interfaces/OrderRequests/OrderRequestSearchParams';
import { OrderRequestWithProducts } from '../../../models/interfaces/OrderRequests/OrderRequestWithProducts';
import { OrderRequestsService } from '../../../services/order-requests.service';

@Component({
  selector: 'app-order-requests',
  templateUrl: './order-requests.component.html',
  styleUrls: ['./order-requests.component.scss']
})
export class OrderRequestsComponent implements OnInit {

  orderRequests: OrderRequestWithProducts[];

  searchParams: OrderRequestSearchParams;
  defaultSearchParams: OrderRequestSearchParams = {
    Email: null,
    Address: null,
    FIO: null,
    PageNumber: 1,
    PageSize: 10
  }

  constructor(private orderREquestService: OrderRequestsService) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getOrderRequests();
  }

  getOrderRequests() {
    this.orderREquestService.getAllOrderRequests(this.searchParams).then(results => {
      this.orderRequests = results;
    })
  }

  goToOrderRequestDetails(id: number) {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getOrderRequests();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getOrderRequests();
  }

}
