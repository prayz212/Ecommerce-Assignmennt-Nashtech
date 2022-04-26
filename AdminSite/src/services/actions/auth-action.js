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

export const refreshTokenRequest = (token) => {
  return {
    type: actionTypes.AUTH_REFRESH_TOKEN_REQUEST,
    payload: token
  }
}

export const refreshTokenSuccess = (newToken) => {
  return {
    type: actionTypes.AUTH_REFRESH_TOKEN_SUCCESS,
    payload: newToken
  }
}

export const resetAuthError = () => {
  return {
    type: actionTypes.AUTH_RESET_ERROR
  }
}

export const registerRequest = (formData) => {
  return {
    type: actionTypes.AUTH_REGISTER_REQUEST,
    payload: formData,
  };
}

export const registerSuccess = (tokenData) => {
  return {
    type: actionTypes.AUTH_REGISTER_SUCCESS,
    payload: tokenData,
  };
}

export const registerFail = (error) => {
  return {
    type: actionTypes.AUTH_REGISTER_FAIL,
    payload: error,
  };
}

export const logoutRequest = () => {
  return {
    type: actionTypes.AUTH_LOGOUT_REQUEST,
  };
};

export const logoutSuccess = () => {
  return {
    type: actionTypes.AUTH_LOGOUT_SUCCESS,
  };
};