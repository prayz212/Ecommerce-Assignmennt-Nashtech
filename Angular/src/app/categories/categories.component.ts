import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NAVIGATE_URL } from '../../constants/navigate-url';
import { CategoriesService } from './categories.service';
import { Category, ICategoryListResponse } from './category.model';

@Component({
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit, OnDestroy {
  createUrl = NAVIGATE_URL.CATEGORIES_CREATE;
  columns: string[] = ['Mã thể loại', 'Tên thể loại', 'Tên hiển thị'];
  categories: Category[] | undefined;
  totalPage: number = 0;
  currentPage: number = 0;
  subscription!: Subscription;

  constructor(private categoriesService: CategoriesService, private router: Router) {}

  ngOnInit(): void {
    this.getAllCategories();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getAllCategories(page?: number): void {
    this.subscription = this.categoriesService
      .getAll(page)
      .subscribe((rawData: ICategoryListResponse) => {
        this.categories = rawData.categories;
        this.totalPage = rawData.total;
        this.currentPage = rawData.current;
      });
  }

  onTableRowClick(id: string): void {
    this.router.navigate([NAVIGATE_URL.CATEGORIES_DETAIL, id]);
  }

  onPageNumberClick(page: number): void {
    this.getAllCategories(page);
  }
}
