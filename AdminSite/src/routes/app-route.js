import React from "react";
import { Routes, Route } from "react-router-dom";
import PrivateRoutes from "./private";
import { PublicRoutes } from "./public";

const AppRoute = ({ routes }) => {
  return (
    <Routes>
      {routes.map((route) => {
        if (route.private) {
          return (
            <Route
              key={route.path}
              path={route.path}
              element={
                <PrivateRoutes
                  component={route.component}
                  requirePermission={route.permission}
                  breadcrumbs={route.breadcrumbs}
                />
              }
            />
          );
        } else {
          return (
            <Route
              key={route.path}
              path={route.path}
              element={<PublicRoutes component={route.component} />}
            />
          );
        }
      })}
    </Routes>
  );
};

export default AppRoute;
