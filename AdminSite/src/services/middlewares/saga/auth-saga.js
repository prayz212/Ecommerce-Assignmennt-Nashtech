import { call, put, takeEvery, takeLatest, delay } from "redux-saga/effects";
import {
  loginFail,
  loginSuccess,
  // logoutSuccess,
  // refreshTokenSuccess,
  // registerFail,
  // registerSuccess,
} from "../../actions/auth-action";
import { type as actionTypes } from "../../constants/auth-constant";
import { authService, systemService } from "../../modules";

// worker Saga: will be fired on AUTH_LOGIN_REQUEST actions
function* loginWorker(action) {
  try {
    const response = yield call(authService.handleLogin, action.payload);
    const { token, expiration } = response.data;

    yield delay(400);
    yield put(loginSuccess({ accessToken: token, expiredTime: expiration }));
  } catch (e) {
    const { status } = JSON.parse(e.message);
    const message =
      status == 400
        ? "Tên tài khoản hoặc mật khẩu không đúng"
        : "Lỗi đăng nhập, vui lòng thử lại";

    yield delay(600);
    yield put(loginFail({ status, message }));
    console.log(e.message); //something like that: {"status":404,"statusText":"Not Found"}
  }
}

// function* refreshTokenWorker(action) {
//   try {
//     const response = yield call(authService.handleRefreshToken, action.payload);
//     const { accessToken, refreshToken, user } = response;
//     yield put(
//       refreshTokenSuccess({ newAccessToken: accessToken, newUser: user })
//     );
//     systemService.saveRefreshToken(refreshToken);
//   } catch (error) {
//     console.log(error.message);
//   }
// }

// function* registerWorker(action) {
//   try {
//     const response = yield call(authService.handleRegister, action.payload);
//     const { accessToken, refreshToken, user } = response;
//     yield put(
//       registerSuccess({ registerAccessToken: accessToken, registerUser: user })
//     );
//     systemService.saveRefreshToken(refreshToken);
//   } catch (error) {
//     const { status } = JSON.parse(error.message);
//     const message =
//       status == 400
//         ? "Tên tài khoản hoặc email đã tồn tại"
//         : "Lỗi đăng ký, vui lòng thử lại sau";

//     yield put(registerFail({ status, message }));
//     console.log(error.message); //something like that: {"status":404,"statusText":"Not Found"}
//   }
// }

// function* logoutWorker() {
//   try {
//     yield call(authService.handleLogout);
//     yield delay(1000);
//     yield put(logoutSuccess());
//   } catch (error) {
//     console.log(error.message);
//   }
// }

function* authSaga() {
  yield takeEvery(actionTypes.AUTH_LOGIN_REQUEST, loginWorker);
}

export default authSaga;
