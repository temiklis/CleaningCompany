import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IdleEmployee } from '../../../models/interfaces/Employees/IdleEmployee';
import { OrderRequestDetails } from '../../../models/interfaces/OrderRequests/OrderRequestDetails';
import { CreateOrder } from '../../../models/interfaces/Orders/CreateOrder';
import { EmployeeService } from '../../../services/employee.service';
import { HelperService } from '../../../services/helper.service';
import { OrderRequestsService } from '../../../services/order-requests.service';
import { OrdersService } from '../../../services/orders.service';

@Component({
  selector: 'app-edit-order-request',
  templateUrl: './edit-order-request.component.html',
  styleUrls: ['./edit-order-request.component.scss']
})
export class EditOrderRequestComponent implements OnInit {

  id: number;
  orderRequest: OrderRequestDetails;

  employees: IdleEmployee[] = [];

  selectedEmployees: string[] = [];

  constructor(private route: ActivatedRoute,
    private router: Router,
    public helperService: HelperService,
    private orderRequestService: OrderRequestsService,
    private employeeService: EmployeeService,
    private orderService: OrdersService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = Number(params.id);
      this.getOrderRequest();
    })
  }

  getOrderRequest() {
    this.orderRequestService.getOrderRequestById(this.id).then(request => {
      this.orderRequest = request;

      this.getAvailiableEmployees();
    })
  }

  getAvailiableEmployees() {
    this.employeeService.getIdleEmployees(this.orderRequest.RequestedDate).then(employees => {
      this.employees = employees;
    })
  }

  goToRequests() {
    this.router.navigate(['/admin/order-requests']);
  }

  employeeChecked(id: string) {
    let index = this.selectedEmployees.findIndex(e => e == id);

    if (index >= 0) {
      this.selectedEmployees.splice(index, 1);
    }
    else {
      this.selectedEmployees.push(id);
    }
  }

  cancel() {
    this.goToRequests();
  }

  save() {
    let newOrder: CreateOrder = {
      OrderRequestId: this.id,
      Employees: this.selectedEmployees
    };

    this.orderService.createOrder(newOrder).then(_ => {
      this.helperService.alert("Order was successfully created!");
      this.goToRequests();
    })
  }
}
