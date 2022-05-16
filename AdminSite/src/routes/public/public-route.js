import { NAVIGATE_URL } from '../../constants/navigate-url';
import {NotFoundPage, SignInPage} from '../../pages';

const NOT_FOUND_ROUTE = {
  path: '*',
  component: <NotFoundPage />,
  private: false,
};

const SIGN_IN_ROUTE = {
  path: NAVIGATE_URL.AUTHENTICATION_SIGN_IN,
  component: <SignInPage />,
  private: false,
};

export default [
  NOT_FOUND_ROUTE,
  SIGN_IN_ROUTE,
];