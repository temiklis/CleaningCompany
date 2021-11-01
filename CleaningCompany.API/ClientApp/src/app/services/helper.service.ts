import { Injectable } from "@angular/core";
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
      }).then(() => {
        resolve();
      });
    });
  }

  alert(message: string): Promise<any> {
    return this.alertWithTitle("", message);
  }

  alertError(message: string): Promise<any> {
    return this.alertWithTitle("Error", message);
  }
}
