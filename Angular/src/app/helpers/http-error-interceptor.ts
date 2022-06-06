import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { AuthService } from "../authentication/auth.service";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((e: HttpErrorResponse) => {
        if (e.status === 401) {
          this.authService.logout();
          location.reload();
        }

        const error: IError = {
          status: e.status,
          title: e.error.title,
          message: e.message,
        };

        throw error;
      })
    )
  }
}

export interface IError {
  status: number;
  title: string;
  message: string;
}
