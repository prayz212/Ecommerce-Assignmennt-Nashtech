import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { EDIT_FORM_TYPE } from "../../constants/variables";
import { categoryService } from "../../services/modules";

const ProductForm = ({ type, handleSubmitForm, item = null }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    categoryService
      .getCategoryList()
      .then((categories) => setCategories(categories));
  }, []);

  return (
    <form className="mx-4 mt-6" onSubmit={handleSubmit(handleSubmitForm)}>
      {type === EDIT_FORM_TYPE && (
        <input
          id="id"
          type="number"
          hidden={true}
          defaultValue={item ? item.id : 0}
          {...register("id")}
        />
      )}

      <div className="relative z-0 w-full mb-6 group">
        <input
          id="name"
          type="text"
          name="product_name"
          placeholder=" "
          defaultValue={item ? item.name : ""}
          className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          {...register("name", {
            required: {
              value: true,
              message: "Tên sản phẩm không được để trống",
            },
            minLength: {
              value: 5,
              message: "Tên sản phẩm phải có tối thiểu 5 ký tự",
            },
          })}
        />
        <label
          htmlFor="product_name"
          className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
        >
          Tên sản phẩm
        </label>
        {errors.name && (errors.name.type === "required" || "minLength") && (
          <span
            className="peer-focus:font-medium text-sm text-red-500"
            role="alert"
          >
            {errors.name.message}
          </span>
        )}
      </div>

      <div className="relative z-0 w-full mb-6 group">
        <input
          type="text"
          name="description"
          placeholder=" "
          defaultValue={item ? item.description : ""}
          className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          {...register("description", {
            required: {
              value: true,
              message: "Mô tả không được để trống",
            },
            minLength: {
              value: 50,
              message: "Mô tả phải có tối thiểu 50 ký tự",
            },
          })}
        />
        <label
          htmlFor="description"
          className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
        >
          Mô tả
        </label>
        {errors.description &&
          (errors.description.type === "required" || "minLength") && (
            <span
              className="peer-focus:font-medium text-sm text-red-500"
              role="alert"
            >
              {errors.description.message}
            </span>
          )}
      </div>

      <div className="flex flex-row mb-4">
        <div className="w-full sm:w-1/2">
          <div className="relative z-0 w-full mb-6 group">
            <input
              type="number"
              name="prices"
              placeholder=" "
              defaultValue={item ? item.prices : ""}
              className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              {...register("prices", {
                required: {
                  value: true,
                  message: "Giá tiền không được để trống",
                },
                valueAsNumber: true,
              })}
            />
            <label
              htmlFor="prices"
              className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Giá tiền
            </label>
            {errors.prices &&
              (errors.prices.type === "required" ||
                "pattern" ||
                "valueAsNumber") && (
                <span
                  className="peer-focus:font-medium text-sm text-red-500"
                  role="alert"
                >
                  {errors.prices.message}
                </span>
              )}
          </div>
        </div>

        <div className="w-full flex flex-row sm:w-1/2 pl-12">
          <label
            htmlFor="Toggle1"
            className="inline-flex items-center space-x-4 cursor-pointer dark:text-gray-100 mb-4"
          >
            <span className="text-sm text-gray-500 dark:text-gray-400">
              Sản phẩm tiêu biểu
            </span>
            <span className="relative">
              <input
                id="Toggle1"
                type="checkbox"
                className="hidden peer"
                {...register("isFeatured")}
              />
              <div className="w-10 h-6 rounded-full shadow-inner dark:bg-gray-600 peer-checked:dark:bg-green-600"></div>
              <div className="absolute inset-y-0 left-0 w-4 h-4 m-1 rounded-full shadow peer-checked:right-0 peer-checked:left-auto dark:bg-black"></div>
            </span>
          </label>
        </div>
      </div>

      {categories && categories.length > 0 && (
        <div className="flex flex-row w-full mb-6 group">
          <label
            htmlFor="categories"
            className="w-40 text-sm text-gray-500 dark:text-gray-400 flex items-center"
          >
            Thể loại sản phẩm
          </label>
          <select
            id="categories"
            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
            {...register("categories", {
              required: {
                value: true,
                message: "Thể loại không được để trống",
              },
            })}
          >
            {categories.map((category) => (
              <option value={category.id}>{category.displayName}</option>
            ))}
          </select>
          {errors.categories &&
            (errors.categories.type === "required" || "validate") && (
              <span
                className="peer-focus:font-medium text-sm text-red-500"
                role="alert"
              >
                {errors.categories.message}
              </span>
            )}
        </div>
      )}

      <div className="flex items-center justify-center w-full mb-6">
        <label
          htmlFor="dropzone-file"
          className="flex flex-col items-center justify-center w-full h-64 border-2 border-gray-300 border-dashed rounded-lg cursor-pointer bg-gray-50 dark:hover:bg-bray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600"
        >
          <div className="flex flex-col items-center justify-center pt-5 pb-6">
            <svg
              className="w-10 h-10 mb-3 text-gray-400"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12"
              ></path>
            </svg>
            <p className="mb-2 text-sm text-gray-500 dark:text-gray-400">
              <span className="font-semibold">Click to upload</span> or drag and
              drop
            </p>
            <p className="text-xs text-gray-500 dark:text-gray-400">
              SVG, PNG, JPG or GIF (MAX. 800x400px)
            </p>
          </div>
          <input id="dropzone-file" type="file" className="hidden" />
        </label>
      </div>

      <div className="flex justify-end">
        <button
          type="submit"
          className="mr-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          {type === EDIT_FORM_TYPE ? "Cập nhật" : "Tạo mới"}
        </button>
      </div>
    </form>
  );
};

export default ProductForm;
