import { ADMIN_PERMISSION, STAFF_PERMISSION } from "../../constants/permission";
import {
  CategoryPage,
  CustomerPage,
  DashboardPage,
  ProductPage,
} from "../../pages";

const PRIVATE_DASHBOARD_ROUTE = {
  path: "/",
  component: <DashboardPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [{ name: "Dashboard", path: "/" }],
};

const PRIVATE_CATEGORY_LIST_ROUTE = {
  path: "/categories",
  component: <CategoryPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [{ name: "Dashboard", path: "/" }, { name: "Quản lý thể loại" }],
};

const PRIVATE_PRODUCT_LIST_ROUTE = {
  path: "/products",
  component: <ProductPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [{ name: "Dashboard", path: "/" }, { name: "Quản lý sản phẩm" }],
};

const PRIVATE_CUSTOMER_LIST_ROUTE = {
  path: "/customers",
  component: <CustomerPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: "/" },
    { name: "Quản lý khách hàng" },
  ],
};

/*
  OBJECTT look like: {
    path: '',
    component: <Component>,
    private: true | false,
    permission: 'admin' | 'staff',
    breadcrumbs: ['dashboard', 'Quản lý sản phẩm', 'Danh sách sản phẩm']
  }
*/

export default [
  PRIVATE_DASHBOARD_ROUTE,
  PRIVATE_CATEGORY_LIST_ROUTE,
  PRIVATE_PRODUCT_LIST_ROUTE,
  PRIVATE_CUSTOMER_LIST_ROUTE,
];
