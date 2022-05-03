import React from "react";
import { EDIT_FORM_TYPE } from "../../constants/variables";
import { useForm } from "react-hook-form";

const CategoryForm = ({ type, handleSubmitForm, item = null }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  return (
    <>
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
            name="category_name"
            placeholder=" "
            defaultValue={item ? item.name : ""}
            className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
            {...register("name", {
              required: {
                value: true,
                message: "Tên thể loại không được để trống",
              },
              minLength: {
                value: 5,
                message: "Tên thể loại phải có tối thiểu 5 ký tự",
              },
            })}
          />
          <label
            htmlFor="category_name"
            className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
          >
            Tên thể loại
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
            id="displayName"
            type="text"
            name="display_name"
            placeholder=" "
            defaultValue={item ? item.displayName : ""}
            className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
            {...register("displayName", {
              required: {
                value: true,
                message: "Tên hiển thị không được để trống",
              },
              minLength: {
                value: 10,
                message: "Tên hiển thị phải có tối thiểu 10 ký tự",
              },
            })}
          />
          <label
            htmlFor="display_name"
            className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
          >
            Hiển thị dưới tên
          </label>
          {errors.displayName &&
            (errors.displayName.type === "required" || "minLength") && (
              <span
                className="peer-focus:font-medium text-sm text-red-500"
                role="alert"
              >
                {errors.displayName.message}
              </span>
            )}
        </div>
        <div className="relative z-0 w-full mb-6 group">
          <input
            id="description"
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
                value: 20,
                message: "Mô tả phải có tối thiểu 20 ký tự",
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
        <div className="flex justify-end">
          <button
            type="submit"
            className="mr-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            {type === EDIT_FORM_TYPE ? "Cập nhật" : "Tạo mới"}
          </button>
        </div>
      </form>
    </>
  );
};

export default CategoryForm;
