import axiosRequest from "../../config/http-request";
import urlRequest from "../utils/url-request";

const getClients = async (page, size) => {
  return axiosRequest
    .get(urlRequest.CLIENTS.GET_CLIENT_LIST(page, size))
    .then(({data}) => data)
    .catch((error) => {
      throw new Error(error);
    });
};

export default {
  getClients,
};
