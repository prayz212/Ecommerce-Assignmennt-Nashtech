import _ from "lodash";
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import { MasterPublicLayout } from "../../layouts";

export const PublicRoutes = ({ component, ...rest }) => {
  // @ts-ignore
  const token = useSelector((state) => state.auth.accessToken);
  if (!_.isEmpty(token)) {
    return <Navigate to={NAVIGATE_URL.DASHBORAD} />;
  }

  return (
    <MasterPublicLayout
      {...rest}
      component={component}
    />
  );
};
