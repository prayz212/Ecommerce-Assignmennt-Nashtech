import {type as actionTypes} from '../constants/auth-constant';

export const loginRequest = (formData) => {
  return {
    type: actionTypes.AUTH_LOGIN_REQUEST,
    payload: formData
  };
};

export const loginSuccess = (tokenData) => {
  return {
    type: actionTypes.AUTH_LOGIN_SUCCESS,
    payload: tokenData
  };
};

export const loginFail = (error) => {
  return {
    type: actionTypes.AUTH_LOGIN_FAIL,
    payload: error
  }
}

export const logoutRequest = () => {
  return {
    type: actionTypes.AUTH_LOGOUT_REQUEST,
  };
};