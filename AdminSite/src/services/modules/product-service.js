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

export default {
  getProductList,
  getProductDetail,
};
