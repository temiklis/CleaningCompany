import { Injectable } from "@angular/core";
import { OrderRequest } from "../models/interfaces/OrderRequests/OrderRequest";
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
}
