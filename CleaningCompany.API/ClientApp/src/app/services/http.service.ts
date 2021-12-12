import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class HttpService {
  apiBaseUrl: string = "http://localhost:5000/api/";

  constructor(
    private _http: HttpClient,
    private router: Router,
  ) { }

  GET<T = {}>(url: string, params?: any): Promise<T> {
    return new Promise((success, fail) => {
      this._http
        .get(this.apiBaseUrl + url, {
          headers: this.applyHeaders(),
          params: params,
          observe: 'response'
        }).subscribe(resp => {
          var value = resp.headers.get('X-Pagination');
          localStorage.setItem('X-Pagination', value);

          success(resp.body as Promise<T>);
        })
    });
  }

  POST(url: string, data: any): Promise<{}> {
    return new Promise((success, fail) => {
      this._http
        .post(this.apiBaseUrl + url, data, {
          headers: this.applyHeaders(),
        })
        .subscribe(
          (results) => {
            success(results);
          },
          (error) => {
            if (error.status === 401 || error.status === 403) {
              this.redirectToLogin();
            } else if (error.status === 200) {
              success(error.error.text);
            } else {
              fail(error);
            }
          }
        );
    });
  }

  PUT(url: string, data: any): Promise<{}> {
    return new Promise((success, fail) => {
      this._http
        .put(this.apiBaseUrl + url, data, {
          headers: this.applyHeaders(),
        })
        .subscribe(
          (results) => {
            success(results);
          },
          (error) => {
            if (error.status === 401 || error.status === 403) {
              this.redirectToLogin();
            } else if (error.status === 200) {
              success(error.error.text);
            } else {
              fail(error);
            }
          }
        );
    });
  }

  DELETE(url: string): Promise<{}> {
    return new Promise((success, fail) => {
      this._http
        .delete(this.apiBaseUrl + url, {
          headers: this.applyHeaders(),
        })
        .subscribe(
          (results) => {
            success(results);
          },
          (error) => {
            if (error.status === 401 || error.status === 403) {
              this.redirectToLogin();
            } else if (error.status === 200) {
              success(error.error.text);
            } else {
              fail(error);
            }
          }
        );
    });
  }

  private redirectToLogin() {
    console.log("UnAuthorized!");
    /*this.authService.redirectToLogin();*/
  }

  private redirectToNotFound() {
    console.log("NotFound!");
    /*this.router.navigate(['/404']);*/
  }

  private canApplyAuthHeader() {
    /*return !this.helperService.noe(this.authService.getAccessToken());*/
  }

  private applyHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    /*if (this.canApplyAuthHeader()) {
      headers = headers.set("Authorization", "Bearer " + this.authService.getAccessToken())
        .delete("Content-Type");
    }*/
    return headers;
  }

}
