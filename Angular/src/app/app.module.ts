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
import { HttpClientModule } from '@angular/common/http';
import { PaginationComponent } from './pagination/pagination.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { FormsModule } from '@angular/forms';
import { ToastComponent } from './toast/toast.component';
import { CategoryFormComponent } from './categories/category-form/category-form.component';
import { CategoriesEditComponent } from './categories-edit/categories-edit.component';
import { ModalComponent } from './modal/modal.component';

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
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [
    CategoriesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
