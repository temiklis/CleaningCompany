import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/interfaces/Orders/Order';
import { OrderSearchParams } from '../../../models/interfaces/Orders/OrderSearchParams';
import { XPagination } from '../../../models/interfaces/X-Pagination';
import { HelperService } from '../../../services/helper.service';
import { OrdersService } from '../../../services/orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: Order[];

  searchParams: OrderSearchParams;
  defaultSearchParams: OrderSearchParams = {
    PageNumber: 1,
    PageSize: 10
  }

  pagination: XPagination;

  constructor(private orderService: OrdersService,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getOrders();
  }

  getOrders() {
    this.orderService.getAllOrders(this.searchParams).then(results => {
      this.orders = results;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToCreateOrder() {

  }

  goToOrderDetails(id: number) {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getOrders();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getOrders();
  }


}
