import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoriesComponent } from './categories/categories.component';
import { TopSectionComponent } from './top-section/top-section.component';
import { CategoriesCreateComponent } from './categories-create/categories-create.component';
import { TableComponent } from './table/table.component';
import { CategoriesService } from './categories/categories.service';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { PaginationComponent } from './pagination/pagination.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { FormsModule } from '@angular/forms';
import { ToastComponent } from './toast/toast.component';
import { CategoryFormComponent } from './categories/category-form/category-form.component';
import { CategoriesEditComponent } from './categories-edit/categories-edit.component';
import { ModalComponent } from './modal/modal.component';
import { LoginComponent } from './authentication/login.component';
import { LoginFormComponent } from './authentication/login-form/login-form.component';
import { AuthenticationComponent } from './layouts/authentication/authentication.component';
import { MainComponent } from './layouts/main/main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JwtInterceptor } from './helpers/jwt-interceptor';
import { HttpErrorInterceptor } from './helpers/http-error-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoriesComponent,
    TopSectionComponent,
    CategoriesCreateComponent,
    TableComponent,
    PaginationComponent,
    CategoryDetailComponent,
    ToastComponent,
    CategoryFormComponent,
    CategoriesEditComponent,
    ModalComponent,
    LoginComponent,
    LoginFormComponent,
    AuthenticationComponent,
    MainComponent,
    DashboardComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [
    CategoriesService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
