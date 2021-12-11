import { Injectable } from "@angular/core";
import { ClientOrder } from "../models/interfaces/Orders/ClientOrder";
import { EmployeeOrder } from "../models/interfaces/Orders/EmployeeOrder";
import { Order } from "../models/interfaces/Orders/Order";
import { OrderSearchParams } from "../models/interfaces/Orders/OrderSearchParams";
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
}
