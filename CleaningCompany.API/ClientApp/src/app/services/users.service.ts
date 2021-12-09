import { Injectable } from "@angular/core";
import { UpdateUserProfile } from "../models/interfaces/Users/UpdateUserProfile";
import { UserProfile } from "../models/interfaces/Users/UserProfile";
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

  getRoles(): Promise<string[]> {
    return this.httpService.GET<string[]>(`User/Roles`);
  }

  getUserProfile(): Promise<UserProfile> {
    return this.httpService.GET<UserProfile>(`User`);
  }

  updateUserProfile(userProfile: UpdateUserProfile): Promise<any> {
    return this.httpService.PUT(`User`, userProfile);
  }
}
