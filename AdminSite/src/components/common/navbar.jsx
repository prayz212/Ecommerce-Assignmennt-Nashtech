import React from "react";
import { Link, useLocation } from "react-router-dom";
import { NAVIGATE_URL } from "../../constants/navigate-url";

const Navbar = ({ onSignOut }) => {
  const { pathname } = useLocation();

  return (
    <div className="flex h-full">
      <div className="p-3 space-y-2 w-60 bg-gray-800 text-slate-100">
        <div className="flex items-center p-2 space-x-4">
          <img
            src="https://source.unsplash.com/100x100/?portrait"
            alt=""
            className="w-12 h-12 rounded-full dark:bg-coolGray-500"
          />
          <div>
            <h2 className="text-lg font-semibold">Leroy Jenkins</h2>
            <span className="flex items-center space-x-1 text-xs text-coolGray-400">
              System admin
            </span>
          </div>
        </div>
        <div className="divide-y divide-coolGray-700">
          <ul className="pt-2 pb-4 space-y-1 text-sm">
            <li
              className={`${
                pathname === "/" ? "bg-gray-900" : ""
              } hover:bg-gray-700`}
            >
              <Link
                to={NAVIGATE_URL.DASHBORAD}
                className="flex items-center p-2 space-x-3 rounded-md"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 512 512"
                  className="w-5 h-5 fill-current dark:text-coolGray-400"
                >
                  <path d="M68.983,382.642l171.35,98.928a32.082,32.082,0,0,0,32,0l171.352-98.929a32.093,32.093,0,0,0,16-27.713V157.071a32.092,32.092,0,0,0-16-27.713L272.334,30.429a32.086,32.086,0,0,0-32,0L68.983,129.358a32.09,32.09,0,0,0-16,27.713V354.929A32.09,32.09,0,0,0,68.983,382.642ZM272.333,67.38l155.351,89.691V334.449L272.333,246.642ZM256.282,274.327l157.155,88.828-157.1,90.7L99.179,363.125ZM84.983,157.071,240.333,67.38v179.2L84.983,334.39Z"></path>
                </svg>
                <span>Dashboard</span>
              </Link>
            </li>
            <li
              className={`${
                pathname === "/categories" ? "bg-gray-900" : ""
              } hover:bg-gray-700`}
            >
              <Link
                to={NAVIGATE_URL.CATEGORIES_LIST}
                className="flex items-center p-2 space-x-3 rounded-md"
              >
                <svg
                  className="flex-shrink-0 w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path d="M5 3a2 2 0 00-2 2v2a2 2 0 002 2h2a2 2 0 002-2V5a2 2 0 00-2-2H5zM5 11a2 2 0 00-2 2v2a2 2 0 002 2h2a2 2 0 002-2v-2a2 2 0 00-2-2H5zM11 5a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V5zM11 13a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z"></path>
                </svg>
                <span>Quản lý thể loại</span>
              </Link>
            </li>
            <li
              className={`${
                pathname === "/products" ? "bg-gray-900" : ""
              } hover:bg-gray-700`}
            >
              <Link
                to={NAVIGATE_URL.PRODUCT_LIST}
                className="flex items-center p-2 space-x-3 rounded-md"
              >
                <svg
                  className="flex-shrink-0 w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path d="M10 2a4 4 0 00-4 4v1H5a1 1 0 00-.994.89l-1 9A1 1 0 004 18h12a1 1 0 00.994-1.11l-1-9A1 1 0 0015 7h-1V6a4 4 0 00-4-4zm2 5V6a2 2 0 10-4 0v1h4zm-6 3a1 1 0 112 0 1 1 0 01-2 0zm7-1a1 1 0 100 2 1 1 0 000-2z"></path>
                </svg>
                <span>Quản lý sản phẩm</span>
              </Link>
            </li>
            <li
              className={`${
                pathname === "/customers" ? "bg-gray-900" : ""
              } hover:bg-gray-700`}
            >
              <Link
                to={NAVIGATE_URL.CUSTOMER_LIST}
                className="flex items-center p-2 space-x-3 rounded-md"
              >
                <svg
                  className="flex-shrink-0 w-6 h-6 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z"></path>
                </svg>
                <span>Quản lý khách hàng</span>
              </Link>
            </li>
          </ul>
          <ul className="pt-4 pb-2 space-y-1 text-sm">
            <li className="hover:bg-gray-700">
              <button
                onClick={() => onSignOut()}
                className="flex items-center p-2 space-x-3 rounded-md"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 512 512"
                  className="w-5 h-5 fill-current dark:text-coolGray-400"
                >
                  <path d="M440,424V88H352V13.005L88,58.522V424H16v32h86.9L352,490.358V120h56V456h88V424ZM320,453.642,120,426.056V85.478L320,51Z"></path>
                  <rect width="32" height="64" x="256" y="232"></rect>
                </svg>
                <span>Đăng xuất</span>
              </button>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
