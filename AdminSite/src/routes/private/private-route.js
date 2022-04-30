import { NAVIGATE_URL } from "../../constants/navigate-url";
import { ADMIN_PERMISSION, STAFF_PERMISSION } from "../../constants/permission";
import {
  CategoryPage,
  CustomerPage,
  DashboardPage,
  ProductPage,
  CreateCategoryPage,
  EditCategoryPage,
  CreateProductPage,
} from "../../pages";

const PRIVATE_DASHBOARD_ROUTE = {
  path: NAVIGATE_URL.DASHBORAD,
  component: <DashboardPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [{ name: "Dashboard", path: NAVIGATE_URL.DASHBORAD }],
};

const PRIVATE_CATEGORY_LIST_ROUTE = {
  path: NAVIGATE_URL.CATEGORIES_LIST,
  component: <CategoryPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý thể loại" },
  ],
};

const PRIVATE_CATEGORY_CREATE_ROUTE = {
  path: NAVIGATE_URL.CATEGORIES_CREATE,
  component: <CreateCategoryPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý thể loại", path: NAVIGATE_URL.CATEGORIES_LIST },
    { name: "Tạo mới thể loại" },
  ],
};

const PRIVATE_CATEGORY_EDIT_ROUTE = {
  path: NAVIGATE_URL.CATEGORIES_EDIT,
  component: <EditCategoryPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý thể loại", path: NAVIGATE_URL.CATEGORIES_LIST },
    { name: "Chỉnh sửa thể loại" },
  ],
};

const PRIVATE_PRODUCT_LIST_ROUTE = {
  path: NAVIGATE_URL.PRODUCT_LIST,
  component: <ProductPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý sản phẩm" },
  ],
};

const PRIVATE_PRODUCT_CREATE_ROUTE = {
  path: NAVIGATE_URL.PRODUCT_CREATE,
  component: <CreateProductPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý sản phẩm", path: NAVIGATE_URL.PRODUCT_LIST },
    { name: "Tạo mới sản phẩm" },
  ],
};

const PRIVATE_PRODUCT_EDIT_ROUTE = {
  path: NAVIGATE_URL.PRODUCT_EDIT,
  component: <div>PRIVATE_PRODUCT_EDIT_ROUTE</div>,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
    { name: "Quản lý sản phẩm", path: NAVIGATE_URL.PRODUCT_LIST },
    { name: "Chỉnh sửa sản phẩm" },
  ],
};

const PRIVATE_CUSTOMER_LIST_ROUTE = {
  path: NAVIGATE_URL.CUSTOMER_LIST,
  component: <CustomerPage />,
  private: true,
  permission: [ADMIN_PERMISSION, STAFF_PERMISSION],
  breadcrumbs: [
    { name: "Dashboard", path: NAVIGATE_URL.DASHBORAD },
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
  PRIVATE_CATEGORY_CREATE_ROUTE,
  PRIVATE_CATEGORY_EDIT_ROUTE,
  PRIVATE_PRODUCT_CREATE_ROUTE,
  PRIVATE_PRODUCT_EDIT_ROUTE,
];
