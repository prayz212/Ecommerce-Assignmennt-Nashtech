import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import APIs from '../../utils/url-request';
import { Category, ICategoryListResponse } from './category.model';
import { IHttpResponse } from 'src/utils/http-response.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private _currentValue: Category | undefined;
  constructor(private httpClient: HttpClient) {}

  set currentValue(category: Category) {
    this._currentValue = category;
  }
  get currentValue(): Category {
    return this._currentValue || new Category();
  }

  getAll(page?: number, size?: number): Observable<ICategoryListResponse> {
    return this.httpClient
      .get<any>(APIs.CATEGORY.GET_CATEGORY_LIST(page, size))
      .pipe(
        map((rawData: any): ICategoryListResponse => {
          return {
            total: rawData.totalPage,
            current: rawData.currentPage,
            categories: rawData.categories.map(
              (category: any): Category => Category.formatData(category)
            ),
          };
        })
      );
  }

  getDetail(id: number): Observable<Category> {
    return this.httpClient
      .get<Category>(APIs.CATEGORY.GET_CATEGORY_DETAIL(id))
      .pipe(
        map((rawData: any): Category => {
          return Category.formatData(rawData);
        })
      );
  }

  createCategory(newCategory: any): Observable<IHttpResponse> {
    return this.httpClient
      .post<IHttpResponse>(APIs.CATEGORY.CREATE_CATEGORY(), newCategory, {
        observe: 'response',
      })
      .pipe(
        catchError((err: HttpErrorResponse) => {
          console.warn(err);
          throw new Error(err.message);
        })
      );
  }

  updateCategory(updateCategory: any): Observable<IHttpResponse> {
    return this.httpClient
      .put<IHttpResponse>(APIs.CATEGORY.UPDATE_CATEGORY(), updateCategory, {
        observe: 'response',
      })
      .pipe(
        catchError((err: HttpErrorResponse) => {
          console.warn(err);
          throw new Error(err.message);
        })
      );
  }

  deleteCategory(id: number): Observable<IHttpResponse> {
    return this.httpClient
      .delete<IHttpResponse>(APIs.CATEGORY.DELETE_CATEGORY(id), {
        observe: 'response',
      })
      .pipe(
        catchError((err: HttpErrorResponse) => {
          console.warn(err);
          throw new Error(err.message);
        })
      );
  }
}
