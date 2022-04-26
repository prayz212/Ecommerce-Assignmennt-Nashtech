import store from "../store";
import { BASE_URL } from "../../constants/http";
import axiosRequest from "../../config/http-request";
import { refreshTokenRequest } from "../../services/actions/auth-action";
import { systemService } from "../../services/modules";

const handleLogin = (formInput) => {
  return axiosRequest
    .post(`${BASE_URL}/auth/login`, formInput)
    .then((token) => token)
    .catch((err) => {
      throw new Error(err);
    });
};

const getAcessToken = () => {
  const state = store.getState();
  const { accessToken, expiredTime } = state.auth;

  if (Date.parse(expiredTime) < Date.now()) {
    const refreshToken = systemService.getRefreshToken();

    if (refreshToken) {
      store.dispatch(refreshTokenRequest({ token: refreshToken }));
    }

    return refreshToken ? state.auth.accessToken : null;
  }

  return accessToken;
};

const handleRefreshToken = (token) => {
  return axiosRequest
    .post(`${BASE_URL}/auth/refresh-token`, token)
    .then((token) => token)
    .catch((err) => {
      throw new Error(err);
    });
};

const handleRegister = (formInput) => {
  return axiosRequest
    .post(`${BASE_URL}/auth/register`, formInput)
    .then((res) => res)
    .catch((error) => {
      throw new Error(error);
    });
};

const handleLogout = () => {
  systemService.deleteRefreshToken();
}

export default {
  handleLogin,
  getAcessToken,
  handleRefreshToken,
  handleRegister,
  handleLogout,
};
