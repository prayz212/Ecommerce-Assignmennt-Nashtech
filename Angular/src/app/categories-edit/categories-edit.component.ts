import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NAVIGATE_URL } from 'src/constants/navigate-url';
import { EDIT_FORM_TYPE } from 'src/constants/variables';
import { CategoriesService } from '../categories/categories.service';
import { Category } from '../categories/category.model';

@Component({
  selector: 'app-categories-edit',
  templateUrl: './categories-edit.component.html',
  styleUrls: ['./categories-edit.component.css'],
})
export class CategoriesEditComponent implements OnInit, OnDestroy {
  preUrl!: string;
  formType = EDIT_FORM_TYPE;
  category!: Category;
  subscription: Subscription | undefined;
  errorMessage: string | undefined;

  constructor(
    private categoriesService: CategoriesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.category = this.categoriesService.currentValue;
    this.preUrl = `${NAVIGATE_URL.CATEGORIES_DETAIL}/${this.category.id}`
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onSubmit(formData: any): void {
    const { valid, data } = formData;
    if (valid) {
      this.subscription = this.categoriesService
        .updateCategory(data)
        .subscribe({
          next: () => {
            this.router.navigate([NAVIGATE_URL.CATEGORIES_LIST]);
          },
          error: (error: Error) => {
            console.warn(error);
            this.errorMessage = 'Đã xảy ra lỗi, vui lòng thử lại sau.';
          },
        });
    } else {
      this.errorMessage = 'Thông tin form không hợp lệ.';
    }
  }
}
