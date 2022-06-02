import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NAVIGATE_URL } from 'src/constants/navigate-url';
import { IHttpResponse } from 'src/utils/http-response.model';
import { CategoriesService } from '../categories/categories.service';
import { Category } from '../categories/category.model';

@Component({
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css'],
})
export class CategoryDetailComponent implements OnInit, OnDestroy {
  preUrl: string = NAVIGATE_URL.CATEGORIES_LIST;
  categoryId!: number;
  category!: Category;
  subscription!: Subscription;
  isShowModal: boolean = false;
  errorMessage: string | undefined;

  confirmMessages!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private categoriesService: CategoriesService
  ) {}

  ngOnInit(): void {
    this.categoryId = Number(this.route.snapshot.paramMap.get('id')) || 0;
    this.getData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getData(): void {
    this.subscription = this.categoriesService
      .getDetail(this.categoryId)
      .subscribe((c: Category): void => {
        this.category = c;
        this.confirmMessages = `Bạn có chắc muốn xóa ${this.category.displayName}?`;
      });
  }

  onEditClicked(category: Category): void {
    this.router.navigate([NAVIGATE_URL.CATEGORIES_EDIT]);
    this.categoriesService.currentValue = category;
  }

  onDeleteClicked(): void {
    this.isShowModal = true;
  }

  onModalConfirmClick(): void {
    this.isShowModal = false;
    this.subscription = this.categoriesService.deleteCategory(Number(this.category.id)).subscribe({
      next: (response: IHttpResponse): void => {
        if (response.ok)
          this.router.navigate([NAVIGATE_URL.CATEGORIES_LIST]);
      },
      error: (error: Error): void => {
        console.warn(error);
        this.errorMessage = 'Đã xảy ra lỗi, vui lòng thử lại sau.';
      }
    });
  }

  onModalCancelClick(): void {
    this.isShowModal = false;
  }
}
