import { Injectable } from "@angular/core";
import * as moment from 'moment';
import Swal from "sweetalert2";

@Injectable({
  providedIn: 'root'
})
export class HelperService {
  constructor() {

  }

  alertWithTitle(title: string, message: string) {
    if (Swal.isVisible()) {
      var container = Swal.getHtmlContainer();

      if (container.innerHTML == message) {
        return;
      }
    }

    return new Promise((resolve) => {
      Swal.fire({
        title: title,
        html: message,
        buttonsStyling: false,
      })
    });
  }

  alert(message: string): Promise<any> {
    return this.alertWithTitle("", message);
  }

  alertError(message: string): Promise<any> {
    return this.alertWithTitle("Error", message);
  }

  confirmPopup(title: string, message: string): Promise<boolean> {
    return new Promise((resolve) => {
      Swal.fire({
        title: title,
        text: message,
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "YES",
        cancelButtonText: "NO",
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        buttonsStyling: false,
      }).then((result) => {
        if (result.value) {
          resolve(true);
        } else {
          resolve(false);
        }
      });
    });
  }

  toDBDate(date: Date) {
    return moment(date).add(0 - date.getTimezoneOffset(), 'm').utc().toISOString();
  }
}
