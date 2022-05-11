import { useNavigate } from "react-router-dom";
import _ from "lodash";
import { LoginForm } from "../../components/form/login-form";
import { useDispatch, useSelector } from "react-redux";
import { loginRequest } from "../../services/actions/auth-action";
import LoadingPage from "../loaders/loading-page";
import { useEffect, useState } from "react";
import { NAVIGATE_URL } from "../../constants/navigate-url";

const SignInPage = () => {
  // @ts-ignore
  const token = useSelector((state) => state.auth.accessToken);
  // @ts-ignore
  const authError = useSelector((state) => state.auth.error);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (token) navigate(NAVIGATE_URL.DASHBORAD);
  }, [token]);

  useEffect(() => {
    if (!_.isEmpty(authError)) {
      setIsLoading(false);
    }
  }, [authError]);

  const onSubmitLoginForm = (data) => {
    setIsLoading(true);
    dispatch(loginRequest(data));
  };

  return isLoading ? (
    <LoadingPage backgroundColor="bg-gray-800" opacityValue={100} />
  ) : (
    <>
      <div className="min-h-full grid grid-cols-3 w-full">
        <div className="hidden md:col-span-2 md:flex bg-signin-signup bg-no-repeat h-full w-full bg-cover"></div>

        <LoginForm
          handleSubmitForm={(data) => onSubmitLoginForm(data)}
          authError={authError}
        />
      </div>
    </>
  );
};

export default SignInPage;
