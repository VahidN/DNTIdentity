import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { APP_CONFIG, IAppConfig } from "@app/core";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { Request } from "./../models/request";

@Injectable()
export class RequestService {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private appConfig: IAppConfig) { }

  add(model: Request): Observable<any> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    const url = `${this.appConfig.apiEndpoint}/request/add`;
    return this.http
      .post(url, model, { headers: headers })
      .pipe(
        map(response => response || {}),
        catchError((error: HttpErrorResponse) => throwError(error))
      );
  }
}
