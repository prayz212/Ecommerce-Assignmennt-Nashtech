import { BASE_URL } from "../../constants/http";

const GET_CATEGORY_LIST = (page = 1, size= 10) => `${BASE_URL}/categories?page=${page}&size=${size}`;
const GET_CATEGORY_DETAIL = (id) => `${BASE_URL}/categories/${id}`;
const DELETE_CATEGORY = (id) => `${BASE_URL}/categories/${id}`;
const UPDATE_CATEGORY = () => `${BASE_URL}/categories`;
const CREATE_CATEGORY = () => `${BASE_URL}/categories`;

const GET_PRODUCT_LIST = (page = 1, size = 10) => `${BASE_URL}/products?page=${page}&size=${size}`;
const GET_PRODUCT_DETAIL = (id) => `${BASE_URL}/products/${id}`;

export default {
    CATEGORY: {
        GET_CATEGORY_LIST,
        GET_CATEGORY_DETAIL,
        DELETE_CATEGORY,
        UPDATE_CATEGORY,
        CREATE_CATEGORY,
    },
    PRODUCT: {
        GET_PRODUCT_LIST,
        GET_PRODUCT_DETAIL,
    },
}