import { type as actionTypes } from "../constants/auth-constant";

const initialState = {
  accessToken: null,
  expiredTime: null,
  error: {},
};

export const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.AUTH_LOGIN_REQUEST:
      return {
        ...state,
      };
    case actionTypes.AUTH_LOGIN_SUCCESS:
      const { accessToken, expiredTime } = action.payload;
      return {
        ...state,
        accessToken,
        expiredTime,
        error: {},
      };
    case actionTypes.AUTH_LOGIN_FAIL:
      const { status, message } = action.payload;

      return {
        ...state,
        error: {
          status,
          message,
        },
      };
    case actionTypes.AUTH_LOGOUT_REQUEST:
      return {
        ...initialState,
      };
    default:
      return state;
  }
};
