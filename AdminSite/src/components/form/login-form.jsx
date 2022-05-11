import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import _ from "lodash";

export const LoginForm = ({ handleSubmitForm, authError = null }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setFocus,
  } = useForm();

  const [serverError, setServerError] = useState(authError.messgae);

  useEffect(() => {
    if (!_.isEmpty(authError)) {
      const { message } = authError;
      setFocus("username");

      setServerError(message);
    }
  }, [authError]);

  const handleLogin = (data) => {
    handleSubmitForm(data);
  };

  const handleOnChange = () => {
    if (!_.isEmpty(serverError)) {
      setServerError("");
    }
  };

  return (
    <div className="py-2 col-span-3 md:col-span-1 flex items-center justify-center px-4 sm:px-6 lg:px-8">
      <div className="max-w-md lg:w-4/5 space-y-8">
        <div>
          <h2 className="mt-2 text-center text-3xl font-extrabold text-gray-900">
            Đăng nhập tài khoản
          </h2>
        </div>
        <form className="mt-8 space-y-6" onSubmit={handleSubmit(handleLogin)}>
          <input type="hidden" name="remember" defaultValue="true" />
          <div className="rounded-md -space-y-px">
            <div>
              <label
                htmlFor="username"
                className="block text-sm font-medium text-gray-700"
              >
                Tên tài khoản
              </label>
              <input
                type="text"
                className="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 shadow-sm rounded-md focus:outline-none focus:ring-teal-500 focus:border-teal-500 focus:z-10 sm:text-sm"
                placeholder="Tên tài khoản"
                onChangeCapture={handleOnChange}
                {...register("username", {
                  required: {
                    value: true,
                    message: "Tên tài khoản không được bỏ trống",
                  },
                })}
              />

              {errors.username && errors.username.type === "required" && (
                <span className="text-red-500" role="alert">
                  {errors.username.message}
                </span>
              )}
            </div>

            <div className="pt-3">
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700"
              >
                Mật khẩu
              </label>
              <input
                type="password"
                autoComplete="current-password"
                className="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 shadow-sm rounded-md focus:outline-none focus:ring-teal-500 focus:border-teal-500 focus:z-10 sm:text-sm"
                placeholder="Mật khẩu"
                onChangeCapture={handleOnChange}
                {...register("password", {
                  required: {
                    value: true,
                    message: "Mật khẩu không được bỏ trống",
                  },
                  minLength: {
                    value: 8,
                    message: "Mật khẩu cần ít nhất 8 ký tự",
                  },
                })}
              />

              {errors.password &&
                (errors.password.type === "required" || "minLength") && (
                  <span className="text-red-500" role="alert">
                    {errors.password.message}
                  </span>
                )}

              {!errors.password && serverError && (
                <span className="text-red-500" role="alert">
                  {serverError}
                </span>
              )}
            </div>
          </div>
          <button
            type="submit"
            className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-teal-600 hover:bg-teal-700 focus:outline-none focus:ring-2 focus:ring-offset-1 focus:ring-teal-500"
          >
            <span className="absolute left-0 inset-y-0 flex items-center pl-3"></span>
            Đăng nhập
          </button>
        </form>
      </div>
    </div>
  );
};
