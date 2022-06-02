import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NAVIGATE_URL } from 'src/constants/navigate-url';
import { CREATE_FORM_TYPE } from 'src/constants/variables';
import { CategoriesService } from '../categories/categories.service';
@Component({
  templateUrl: './categories-create.component.html',
  styleUrls: ['./categories-create.component.css'],
})
export class CategoriesCreateComponent implements OnInit, OnDestroy {
  preUrl = NAVIGATE_URL.CATEGORIES_LIST;
  formType = CREATE_FORM_TYPE;
  subscription: Subscription | undefined;
  errorMessage: string | undefined;

  constructor(
    private categoriesService: CategoriesService,
    private router: Router
  ) {}

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onSubmit(formData: any): void {
    const {valid, data} = formData;
    if (valid) {
      this.subscription = this.categoriesService
        .createCategory(data)
        .subscribe({
          next: () => {
            this.router.navigate([`/${NAVIGATE_URL.CATEGORIES_LIST}`]);
          },
          error: (error: Error) => {
            console.warn(error);
            this.errorMessage = "Đã xảy ra lỗi, vui lòng thử lại sau.";
          },
        });
    } else {
      this.errorMessage = 'Thông tin form không hợp lệ.';
    }
  }
}
