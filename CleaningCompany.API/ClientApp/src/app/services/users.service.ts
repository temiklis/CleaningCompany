import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: "root",
})
export class UsersService {

  private userEmailStream = new BehaviorSubject<string>(null);

  public userEmail = this.userEmailStream.asObservable();

  constructor(private httpService: HttpService) {

  }

  getCurrentUserEmail(): Promise<string> {
    return this.httpService.GET<string>(`User/CurrentUserEmail`);
  }

  public setUserEmail(email: string) {
    this.userEmailStream.next(email);
  }
}
