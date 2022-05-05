import axiosRequest from "../../config/http-request";
import UrlRequest from "../utils/url-request";

const getProductList = async (page, size) => {
  return axiosRequest
    .get(UrlRequest.PRODUCT.GET_PRODUCT_LIST(page, size))
    .then(({ data }) => {
      const { products, totalPage, currentPage } = data;
      return { products, totalPage, currentPage };
    })
    .catch((error) => {
      throw new Error(error);
    });
};

const getProductDetail = async (id) => {
  return axiosRequest
    .get(UrlRequest.PRODUCT.GET_PRODUCT_DETAIL(id))
    .then(({ data }) => data)
    .catch((error) => {
      throw new Error(error);
    });
};

const createProduct = async (formData) => {
  return axiosRequest
    .post(UrlRequest.PRODUCT.CREATE_PRODUCT(), formData)
    .then(({ data }) => data)
    .catch((error) => {
      throw new Error(error);
    });
};

const updateProduct = async (formData) => {
  return axiosRequest
    .put(UrlRequest.PRODUCT.UPDATE_PRODUCT(), formData)
    .then(({ data }) => data)
    .catch((error) => {
      throw new Error(error);
    });
};

const deleteProduct = async (id) => {
  return axiosRequest
    .delete(UrlRequest.PRODUCT.DELETE_PRODUCT(id))
    .then(({status}) => status === 200)
    .catch(error => {
      throw new Error(error);
    })
};

export default {
  getProductList,
  getProductDetail,
  createProduct,
  updateProduct,
  deleteProduct
};
