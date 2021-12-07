import { Injectable } from "@angular/core";
import { Client } from "../models/interfaces/Clients/Client";
import { ClientSearchParams } from "../models/interfaces/Clients/ClientSearchParams";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class ClientsService {
  constructor(private httpService: HttpService) {

  }

  getAllClients(parameters: ClientSearchParams): Promise<Client[]> {
    let validParameters = {};

    Object.keys(parameters).forEach(key => {
      if (parameters[key] != null && parameters[key] != '' && parameters[key] != 0) {
        validParameters[key] = parameters[key];
      }
    })

    return this.httpService.GET<Client[]>(`Client`, validParameters);
  }
}
