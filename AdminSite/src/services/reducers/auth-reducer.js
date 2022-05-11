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
    // case actionTypes.AUTH_REFRESH_TOKEN_REQUEST:
    //   return {
    //     ...initialState,
    //     loading: true,
    //   };
    // case actionTypes.AUTH_REFRESH_TOKEN_SUCCESS:
    //   const { newAccessToken, newUser } = action.payload;

    //   return {
    //     ...state,
    //     accessToken: newAccessToken.token,
    //     expiredTime: newAccessToken.expiredTime,
    //     user: newUser,
    //     loading: false,
    //     error: {},
    //   };
    // case actionTypes.AUTH_RESET_ERROR:
    //   return {
    //     ...state,
    //     error: {},
    //   };
    // case actionTypes.AUTH_REGISTER_REQUEST:
    //   return {
    //     ...state,
    //     loading: true,
    //     error: {},
    //   };
    // case actionTypes.AUTH_REGISTER_SUCCESS:
    //   const { registerAccessToken, registerUser } = action.payload;
    //   return {
    //     ...state,
    //     accessToken: registerAccessToken.token,
    //     expiredTime: registerAccessToken.expiredTime,
    //     user: registerUser,
    //     loading: false,
    //     error: {},
    //   };
    // case actionTypes.AUTH_REGISTER_FAIL:
    //   const payload = action.payload;

    //   return {
    //     ...state,
    //     loading: false,
    //     error: {
    //       status: payload.status,
    //       message: payload.message,
    //     },
    //   };
    case actionTypes.AUTH_LOGOUT_REQUEST:
      return {
        ...initialState,
      };
    // case actionTypes.AUTH_LOGOUT_SUCCESS:
    //   return {
    //     ...initialState,
    //   };
    default:
      return state;
  }
};
