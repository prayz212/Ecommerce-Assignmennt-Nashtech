import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NAVIGATE_URL } from 'src/constants/navigate-url';
import { IHttpResponse } from 'src/utils/http-response.model';
import { AuthService } from './auth.service';
import { Token } from './login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  subscription: Subscription | undefined;
  errorMessage: string | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onSubmitForm(formData: any):void {
    const { valid, data } = formData;

    if (valid) {
      this.subscription = this.authService.login(data).subscribe({
        next: (result: boolean): void => {
          if (result) {
            this.router.navigate([NAVIGATE_URL.DASHBOARD]);
          }
          else {
            location.reload();
          }
        },
        error: (error: Error): void => {
          console.warn(error);
          this.errorMessage = error.message;
        }
      })
    } else {
      this.errorMessage = 'Thông tin form không hợp lệ.';
    }
  }
}
