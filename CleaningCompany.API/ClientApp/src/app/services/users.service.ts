import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class UsersService {

  constructor(private httpService: HttpService) {

  }

  getCurrentUserEmail(): Promise<string> {
    return this.httpService.GET<string>(`User/CurrentUserEmail`);
  }
}
