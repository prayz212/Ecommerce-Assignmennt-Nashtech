import { lazy } from 'react';
const DashboardPage = lazy(() => import('./home/dashboard-page.jsx'));
const ProductPage = lazy(() => import('./product/product-page.jsx'));
const UnAuthorizationPage = lazy(() => import('./errors/unauthorization-page.jsx'));
const NotFoundPage = lazy(() => import('./errors/notfound-page.jsx'));
const CategoryPage = lazy(() => import('./category/category-page.jsx'));
const CreateCategoryPage = lazy(() => import('./category/create-category-page'));
const EditCategoryPage = lazy(() => import('./category/edit-category-page'));
const CustomerPage = lazy(() => import('./customer/customer-page.jsx'));

export {
  DashboardPage,
  UnAuthorizationPage,
  ProductPage,
  NotFoundPage,
  CategoryPage,
  CreateCategoryPage,
  EditCategoryPage,
  CustomerPage,
};