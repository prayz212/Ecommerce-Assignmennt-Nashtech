import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NAVIGATE_URL } from '../constants/navigate-url';
import { CategoriesCreateComponent } from './categories-create/categories-create.component';
import { CategoriesEditComponent } from './categories-edit/categories-edit.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

const routes: Routes = [
  {
    path: NAVIGATE_URL.CATEGORIES_LIST,
    component: CategoriesComponent,
  },
  {
    path: NAVIGATE_URL.CATEGORIES_CREATE,
    component: CategoriesCreateComponent,
  },
  {
    path: `${NAVIGATE_URL.CATEGORIES_DETAIL}/:id`,
    component: CategoryDetailComponent,
  },
  {
    path: NAVIGATE_URL.CATEGORIES_EDIT,
    component: CategoriesEditComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
