import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Token, Login } from './login.model';
import APIs from '../../utils/url-request';
import { IHttpResponse } from 'src/utils/http-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _currentValue: Token | undefined;

  get currentValue(): Token | undefined {
    return this._currentValue;
  }

  set currentValue(newToken: Token | undefined) {
    this._currentValue = newToken;
  }

  constructor(private httpClient: HttpClient) { }

  login(form: Login): Observable<IHttpResponse> {
    return this.httpClient
      .post<IHttpResponse>(APIs.AUTHENTICATE.SIGN_IN(), form, {
        observe: 'response',
      })
      .pipe(
        catchError((err: HttpErrorResponse) => {
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
}
