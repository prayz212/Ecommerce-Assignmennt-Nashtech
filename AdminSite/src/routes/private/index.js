import React from 'react'
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { UnAuthorizationPage } from '../../pages';
import { MasterLayout } from '../../layouts/index.jsx';
import _ from 'lodash'

const PrivateRoutes = ({ component, requirePermission, breadcrumbs, ...rest }) => {
  // const user = useSelector(state => state.auth.user);
  // if (_.isEmpty(user)) {
  //   return <Navigate to="/sign-in" />;
  // }

  // if (requirePermission.includes(user.permission)) {
  //   return <UnAuthorizationPage />;
  // }

  return <MasterLayout {...rest} component={component} breadcrumbs={breadcrumbs} />;
};

export default PrivateRoutes;