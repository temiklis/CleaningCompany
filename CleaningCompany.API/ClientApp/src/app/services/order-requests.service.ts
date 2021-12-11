import { Injectable } from "@angular/core";
import { ClientOrderRequest } from "../models/interfaces/OrderRequests/ClientOrderRequest";
import { OrderRequest } from "../models/interfaces/OrderRequests/OrderRequest";
import { OrderRequestSearchParams } from "../models/interfaces/OrderRequests/OrderRequestSearchParams";
import { OrderRequestWithProducts } from "../models/interfaces/OrderRequests/OrderRequestWithProducts";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class OrderRequestsService {
  constructor(private httpService: HttpService) {

  }

  createOrderRequest(orderRequest: OrderRequest): Promise<any> {
    return this.httpService.POST(`OrderRequest`, orderRequest);
  }

  getAllOrderRequests(parameters: OrderRequestSearchParams): Promise<OrderRequestWithProducts[]> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<OrderRequestWithProducts[]>('OrderRequest', validParameters);
  }

  getClientOrderRequests(email: string): Promise<ClientOrderRequest[]> {
    return this.httpService.GET<ClientOrderRequest[]>(`OrderRequest/Client?email=${email}`);
  }
}

