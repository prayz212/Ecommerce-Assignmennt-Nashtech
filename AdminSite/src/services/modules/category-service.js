import axiosRequest from "../../config/http-request";
import UrlRequest from "../utils/url-request";


const getCategoryList = async (page, size) => {
	return axiosRequest
		.get(UrlRequest.GET_CATEGORY_LIST(page, size))
		.then(({data}) => data)
		.catch(error => {
			throw new Error(error);
		});
};

const getCategoryDetail = async (id) => {
	return axiosRequest
		.get(UrlRequest.GET_CATEGORY_DETAIL(id))
		.then(({data}) => data)
		.catch(error => {
			throw new Error(error);
		});
};

const deleteCategory = async (id) => {
	return axiosRequest
		.delete(UrlRequest.DELETE_CATEGORY(id))
		.then(({status}) => status === 200)
		.catch(error => {
			throw new Error(error);
		});
};

const updateCategory = async (category) => {
	return axiosRequest
		.put(UrlRequest.UPDATE_CATEGORY(), category)
		.then(({data}) => data)
		.catch(error => {
			throw new Error(error);
		});
};

const createCategory = async (category) => {
	return axiosRequest
		.post(UrlRequest.CREATE_CATEGORY(), category)
		.then(({data}) => data)
		.catch(error => {
			throw new Error(error);
		})
};

export default {
	getCategoryList,
	getCategoryDetail,
	deleteCategory,
	updateCategory,
	createCategory
}