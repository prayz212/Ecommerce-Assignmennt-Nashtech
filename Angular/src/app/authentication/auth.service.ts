import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { Token, Login } from './login.model';
import APIs from '../../utils/url-request';
import { IHttpResponse } from 'src/utils/http-response.model';
import { IError } from '../helpers/http-error-interceptor';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _currentValue: Token | undefined;

  get currentValue(): Token | undefined {
    return this._currentValue;
  }

  constructor(private httpClient: HttpClient) { }

  login(form: Login): Observable<boolean> {
    return this.httpClient
      .post<boolean>(APIs.AUTHENTICATE.SIGN_IN(), form, {
        observe: 'response',
      })
      .pipe(
        map((response: IHttpResponse) => {
          this._currentValue = response.body as Token;
          return response.ok || false;
        }),
        catchError((err: IError) => {
          console.warn(err);

          let messages = "";
          switch (err.status) {
            case 400:
              messages = "Sai tên đăng nhập hoặc mật khẩu.";
              break;
            default:
              messages = "Đã xảy ra lỗi, vui lòng thử lại sau.";
              break;
          }

          throw new Error(messages);
        })
      )
  }

  logout(): void {
    this._currentValue = undefined;
  }
}
