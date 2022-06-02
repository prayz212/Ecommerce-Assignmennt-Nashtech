import { environment } from "../environments/environment";
import { DEFAULT_PAGE_NUMBER, NUMBER_RECORD_PER_PAGE } from "../constants/variables";

const BASE_CLIENT_API = environment.clientUrl;
const BASE_ADMIN_API = environment.adminUrl;

const GET_CATEGORY_LIST = (page = DEFAULT_PAGE_NUMBER, size= NUMBER_RECORD_PER_PAGE) => `${BASE_ADMIN_API}/categories?page=${page}&size=${size}`;
// const GET_ALL_CATEGORIES = () => `${BASE_URL}/client/categories`;
const GET_CATEGORY_DETAIL = (id: number) => `${BASE_ADMIN_API}/categories/${id}`;
const DELETE_CATEGORY = (id: number) => `${BASE_ADMIN_API}/categories/${id}`;
const UPDATE_CATEGORY = () => `${BASE_ADMIN_API}/categories`;
const CREATE_CATEGORY = () => `${BASE_ADMIN_API}/categories`;

// const GET_PRODUCT_LIST = (page = DEFAULT_PAGE_NUMBER, size = NUMBER_RECORD_PER_PAGE) => `${BASE_API_URL}/products?page=${page}&size=${size}`;
// const GET_PRODUCT_DETAIL = (id) => `${BASE_API_URL}/products/${id}`;
// const CREATE_PRODUCT = () => `${BASE_API_URL}/products`;
// const UPDATE_PRODUCT = () => `${BASE_API_URL}/products`;
// const DELETE_PRODUCT = (id) => `${BASE_API_URL}/products/${id}`;

// const UPLOAD_IMAGE = (cloudName) => `${BASE_CLOUDINARY_URL}/${cloudName}/auto/upload`;

// const SIGN_IN = () => `${BASE_URL}/authenticate/login/admin`;

// const GET_CLIENT_LIST = (page = DEFAULT_PAGE_NUMBER, size = NUMBER_RECORD_PER_PAGE) => `${BASE_API_URL}/accounts/clients?page=${page}&size=${size}`;
// const GET_USER_INFO = () => `${BASE_API_URL}/accounts/info`;

export default {
    CATEGORY: {
        GET_CATEGORY_LIST,
    //     GET_ALL_CATEGORIES,
        GET_CATEGORY_DETAIL,
        DELETE_CATEGORY,
        UPDATE_CATEGORY,
        CREATE_CATEGORY,
    },
    // PRODUCT: {
    //     GET_PRODUCT_LIST,
    //     GET_PRODUCT_DETAIL,
    //     CREATE_PRODUCT,
    //     UPDATE_PRODUCT,
    //     DELETE_PRODUCT,
    // },
    // CLOUDINARY: {
    //     UPLOAD_IMAGE,
    // },
    // AUTHENTICATE: {
    //     SIGN_IN,
    // },
    // CLIENTS: {
    //     GET_CLIENT_LIST,
    //     GET_USER_INFO,
    // }
}
