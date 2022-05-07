import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ProductForm from "../../components/form/product-form";
import { CLOUDINARY_CONFIG } from "../../config/cloudinary";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import { EDIT_FORM_TYPE } from "../../constants/variables";
import { cloudinaryService, productService } from "../../services/modules";
import LoadingPage from "../loaders/loading-page";

const EditProductPage = () => {
  const navigate = useNavigate();
  const { state } = useLocation();
  // @ts-ignore
  const { data } = state;
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    return () => {
      setIsLoading(false);
    }
  }, []);

  const onSubmitForm = async (data, images) => {
    setIsLoading(true);

    let imagesInfo = [];
    for (let i = 0; i < images.length; i++) {
      const image = images[i];
      const config = CLOUDINARY_CONFIG(image);

      const resonse = await cloudinaryService.uploadImage(config);
      imagesInfo.push(resonse);
    }

    const formData = { ...data };
    formData.images = imagesInfo;

    productService
      .updateProduct(formData)
      .then(() => navigate(NAVIGATE_URL.PRODUCT_LIST));
  };

  return isLoading ? (
    <LoadingPage />
  ) : (
    <>
      <div className="p-6 pb-2 flex justify-center">
        <div className="text-xl text-slate-100 font-bold text-">
          Biểu mẫu cập nhật sản phẩm
        </div>
      </div>

      <ProductForm
        type={EDIT_FORM_TYPE}
        handleSubmitForm={onSubmitForm}
        item={data}
      />
    </>
  );
};

export default EditProductPage;
