import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NAVIGATE_URL } from '../constants/navigate-url';
import { LoginComponent } from './authentication/login.component';
import { CategoriesCreateComponent } from './categories-create/categories-create.component';
import { CategoriesEditComponent } from './categories-edit/categories-edit.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { UnauthGuard } from './guards/unauth.guard';
import { AuthenticationComponent } from './layouts/authentication/authentication.component';
import { MainComponent } from './layouts/main/main.component';

const routes: Routes = [
  {
    path: NAVIGATE_URL.INDEX,
    component: MainComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: NAVIGATE_URL.DASHBOARD,
        component: DashboardComponent,
      },
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
    ]
  },
  {
    path: NAVIGATE_URL.AUTHENTICATION,
    component: AuthenticationComponent,
    canActivate: [UnauthGuard],
    children: [
      {
        path: NAVIGATE_URL.SIGN_IN,
        component: LoginComponent
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
