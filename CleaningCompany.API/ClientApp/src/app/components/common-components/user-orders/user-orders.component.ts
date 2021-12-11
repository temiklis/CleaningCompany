import { Component, OnInit } from '@angular/core';
import { ClientOrderRequest } from '../../../models/interfaces/OrderRequests/ClientOrderRequest';
import { ClientOrder } from '../../../models/interfaces/Orders/ClientOrder';
import { EmployeeOrder } from '../../../models/interfaces/Orders/EmployeeOrder';
import { AuthorizeService, IUser } from '../../../services/authorization/authorize.service';
import { OrderRequestsService } from '../../../services/order-requests.service';
import { OrdersService } from '../../../services/orders.service';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.scss']
})
export class UserOrdersComponent implements OnInit {

  roles: string[];
  user: IUser;

  clientOrders: ClientOrder[];
  clientRequests: ClientOrderRequest[];

  employeeOrders: EmployeeOrder[];

  constructor(private userService: UsersService,
    private authorizeService: AuthorizeService,
    private orderService: OrdersService,
    private orderRequestService: OrderRequestsService) { }

  ngOnInit(): void {
    this.authorizeService.getUser().subscribe(user => {
      if (user) {
        this.user = user;
      }
    })
    this.getRoles();
  }

  getRoles() {
    this.userService.getRoles().then(roles => {
      this.roles = roles;
      if (this.roles.includes('Employee')) {
        this.getEmployeeOrders();
      }
      else {
        this.getClientRequests();
        this.getClientOrders();
      }
    })
  }

  getEmployeeOrders() {
    this.orderService.getEmployeeAssignedOrders().then(orders => {
      this.employeeOrders = orders;
    })
  }

  getClientRequests() {
    this.orderRequestService.getClientOrderRequests(this.user.name).then(requests => {
      this.clientRequests = requests;
    })
  }

  getClientOrders() {
    this.orderService.getClientOrders().then(orders => {
      this.clientOrders = orders;
    })
  }

}
