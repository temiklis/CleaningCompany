import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderRequestSearchParams } from '../../../models/interfaces/OrderRequests/OrderRequestSearchParams';
import { OrderRequestWithProducts } from '../../../models/interfaces/OrderRequests/OrderRequestWithProducts';
import { XPagination } from '../../../models/interfaces/X-Pagination';
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

  pagination: XPagination;

  constructor(private orderREquestService: OrderRequestsService,
    private router: Router) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getOrderRequests();
  }

  getOrderRequests() {
    this.orderREquestService.getAllOrderRequests(this.searchParams).then(results => {
      this.orderRequests = results;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToOrderRequestDetails(id: number) {
    this.router.navigate([`admin/order-requests/${id}`]);
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
