import { call, put, takeEvery, delay } from "redux-saga/effects";
import {
  loginFail,
  loginSuccess,
} from "../../actions/auth-action";
import { type as actionTypes } from "../../constants/auth-constant";
import { authService } from "../../modules";

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

function* authSaga() {
  yield takeEvery(actionTypes.AUTH_LOGIN_REQUEST, loginWorker);
}

export default authSaga;
