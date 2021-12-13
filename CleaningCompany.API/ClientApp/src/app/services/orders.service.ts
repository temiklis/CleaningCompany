import { Injectable } from "@angular/core";
import { ClientOrder } from "../models/interfaces/Orders/ClientOrder";
import { CreateOrder } from "../models/interfaces/Orders/CreateOrder";
import { EmployeeOrder } from "../models/interfaces/Orders/EmployeeOrder";
import { Order } from "../models/interfaces/Orders/Order";
import { OrderSearchParams } from "../models/interfaces/Orders/OrderSearchParams";
import { UpdateOrderStatus } from "../models/interfaces/Orders/UpdateOrderStatus";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class OrdersService {
  constructor(private httpService: HttpService) {

  }

  getAllOrders(parameters: OrderSearchParams): Promise<Order[]> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<Order[]>(`Order`, validParameters);
  }

  getClientOrders(): Promise<ClientOrder[]> {
    return this.httpService.GET<ClientOrder[]>(`Order/Client`);
  }

  getEmployeeAssignedOrders(): Promise<EmployeeOrder[]> {
    return this.httpService.GET<EmployeeOrder[]>(`Order/Employee`);
  }

  createOrder(order: CreateOrder): Promise<any> {
    return this.httpService.POST(`Order`, order);
  }

  updateOrderStatus(status: UpdateOrderStatus): Promise<any> {
    return this.httpService.PUT(`Order/Status`, status);
  }
}
