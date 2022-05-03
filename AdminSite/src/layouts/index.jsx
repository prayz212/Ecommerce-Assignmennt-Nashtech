import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { logoutRequest } from "../services/actions/auth-action";
import LoadingPage from "../pages/loaders/loading-page";
import Navbar from "../components/common/navbar";
import Breadscrum from "../components/common/breadcrumb";

export const MasterLayout = ({ component, breadcrumbs, ...rest  }) => {
  const user = useSelector((state) => state.auth.user);
  const isLoading = useSelector((state) => state.auth.loading);
  const dispatch = useDispatch();

  const onLogout = () => {
    dispatch(logoutRequest());
  };

  return isLoading ? (
    <LoadingPage />
  ) : (
    <div className="flex flex-row h-screen bg-slate-300">
      <Navbar />
      <div className="flex-1 h-full">
        <div className="p-8 flex flex-col h-screen">
          <Breadscrum breadcrumbs={breadcrumbs} />
          
          <div className="flex-1 mt-4 py-3 px-5 text-gray-700 bg-gray-50 rounded-lg border border-gray-200 dark:bg-gray-800 dark:border-gray-700 overflow-y-scroll">
            {component}
          </div>
        </div>
      </div>
    </div>
  );
};
