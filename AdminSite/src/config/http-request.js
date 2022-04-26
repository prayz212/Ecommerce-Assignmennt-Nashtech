import axios from "axios";
import { HTTP_HEADER, BASE_URL } from "../constants/http";
import { authService } from "../services/modules";

const axiosRequest = axios.create({
  baseURL: BASE_URL,
  headers: HTTP_HEADER,
});

axiosRequest.interceptors.request.use(
  (config) => {
    const accessToken = authService.getAcessToken();
    if (!accessToken) return config;

    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${accessToken}`,
    };

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosRequest.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    const {status, statusText, data} = error.response;
    return Promise.reject(JSON.stringify({status, statusText, data}));
  }
);

export default axiosRequest;