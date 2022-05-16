import _ from "lodash";
import React from "react";
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";
import { NAVIGATE_URL } from "../../constants/navigate-url.js";
import { MasterPrivateLayout } from "../../layouts/private.jsx";

const PrivateRoutes = ({ component, breadcrumbs, ...rest }) => {
  // @ts-ignore
  const token = useSelector((state) => state.auth.accessToken);
  if (_.isEmpty(token)) {
    return <Navigate to={NAVIGATE_URL.AUTHENTICATION_SIGN_IN} />;
  }

  return (
    <MasterPrivateLayout
      {...rest}
      component={component}
      breadcrumbs={breadcrumbs}
    />
  );
};

export default PrivateRoutes;
