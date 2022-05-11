import store from "../store";
import axiosRequest from "../../config/http-request";
import { logoutRequest } from "../../services/actions/auth-action";
import urlRequest from "../utils/url-request";

const handleLogin = (formInput) => {
  return axiosRequest
    .post(urlRequest.AUTHENTICATE.SIGN_IN(), formInput)
    .then((token) => token)
    .catch((err) => {
      throw new Error(err);
    });
};

const getAcessToken = () => {
  const state = store.getState();
  const { accessToken, expiredTime } = state.auth;

  if (Date.parse(expiredTime) < Date.now()) {
    store.dispatch(logoutRequest());
    return null;
  }

  return accessToken;
};

// const handleRefreshToken = (token) => {
//   return axiosRequest
//     .post(`${BASE_URL}/auth/refresh-token`, token)
//     .then((token) => token)
//     .catch((err) => {
//       throw new Error(err);
//     });
// };

// const handleRegister = (formInput) => {
//   return axiosRequest
//     .post(`${BASE_URL}/auth/register`, formInput)
//     .then((res) => res)
//     .catch((error) => {
//       throw new Error(error);
//     });
// };

// const handleLogout = () => {
//   systemService.deleteRefreshToken();
// }

export default {
  handleLogin,
  getAcessToken,
  // handleRefreshToken,
  // handleRegister,
  // handleLogout,
};
