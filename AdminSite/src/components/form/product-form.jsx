import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { EDIT_FORM_TYPE } from "../../constants/variables";
import { categoryService } from "../../services/modules";
import { Modal } from "../common/modal";
import PreviewImageInput from "./preview-image-input";
import UploadImageInput from "./upload-image-input";
import _ from "lodash";

const ProductForm = ({ type, handleSubmitForm, item = null }) => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm();

  const [categories, setCategories] = useState([]);
  const [images, setImages] = useState([]);
  const [isOpenModal, setIsOpenModal] = useState(false);
  const [deletedImages, setDeletedImages] = useState([]);

  useEffect(() => {
    categoryService
      .getAllCategories()
      .then((categories) => setCategories(categories));
  }, []);

  const onInputImageChange = (e) => {
    const files = e.target.files;

    if (files.length > 4) {
      setIsOpenModal(true);
      return;
    }

    setError("images", {
      type: "required",
      message: "",
    });

    //check if total images is greater than 4 or not
    if (type === EDIT_FORM_TYPE) {
      const newImages = [...item.images, ...Array.from(files)];

      if (newImages.length > 4) {
        setIsOpenModal(true);
        return;
      }
    }

    setImages([...Array.from(files)]);
  };

  const onChoosenImageDelete = (image) => {
    const index = images.indexOf(image);
    if (index > -1) {
      const newImages = [...images];
      newImages.splice(index, 1);
      setImages(newImages);
    }

    if (type === EDIT_FORM_TYPE) {
      const index = item.images.indexOf(image);
      if (index < 0) return;
    
      setDeletedImages([...deletedImages, item.images[index]]);
      item.images.splice(index, 1);
    }
  };

  const onSubmitForm = async (data) => {
    if (images.length == 0) {
      setError("images", {
        type: "required",
        message: "Phải chọn tối thiểu 1 ảnh",
      });
      return;
    }

    if (type === EDIT_FORM_TYPE) {
      data.id = item.id;
      data.deletedImages = deletedImages;
    }
    handleSubmitForm(data, images);
  };

  const onModalClose = () => {
    setIsOpenModal(false);
  };

  return isOpenModal ? (
    <Modal isOpen={isOpenModal} onClose={onModalClose} />
  ) : (
    <form className="mx-4 mt-6" onSubmit={handleSubmit(onSubmitForm)}>
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
        <div className="w-full sm:w-1/3">
          <div className="relative z-0 w-full mb-6 group">
            <input
              type="number"
              name="prices"
              placeholder=" "
              defaultValue={item ? item.prices : ""}
              className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
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

        <div className="w-full flex flex-row sm:w-1/3 pl-12">
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
                defaultChecked={(item && item.isFeatured) || false}
                {...register("isFeatured")}
              />
              <div className="w-10 h-6 rounded-full shadow-inner dark:bg-gray-600 peer-checked:dark:bg-green-600"></div>
              <div className="absolute inset-y-0 left-0 w-4 h-4 m-1 rounded-full shadow peer-checked:right-0 peer-checked:left-auto dark:bg-black"></div>
            </span>
          </label>
        </div>

        {categories && categories.length > 0 && (
          <div className="flex flex-row w-full sm:w-1/3 mb-6 group items-center">
            <label
              htmlFor="category"
              className="w-60 text-sm text-gray-500 dark:text-gray-400 flex items-center"
            >
              Thể loại sản phẩm
            </label>
            <select
              id="category"
              className="appearance-none bg-gray-50 h-10 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              value={
                item && item.category
                  ? _.find(categories, { displayName: item.category }).id
                  : categories[0].displayName
              }
              {...register("category", {
                required: {
                  value: true,
                  message: "Thể loại không được để trống",
                },
              })}
            >
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.displayName}
                </option>
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
      </div>

      <UploadImageInput
        {...register("images")}
        errors={errors}
        onInputChange={onInputImageChange}
      />

      <div className="flex">
        <PreviewImageInput
          images={
            type === EDIT_FORM_TYPE ? [...images, ...item.images] : images
          }
          onImageDelete={onChoosenImageDelete}
        />

        <div className="flex items-start">
          <button
            type="submit"
            className="mr-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            {type === EDIT_FORM_TYPE ? "Cập nhật" : "Tạo mới"}
          </button>
        </div>
      </div>
    </form>
  );
};

export default ProductForm;
