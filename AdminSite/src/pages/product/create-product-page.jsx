import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import ProductForm from "../../components/form/product-form";
import { CLOUDINARY_CONFIG } from "../../config/cloudinary";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import { CREATE_FORM_TYPE } from "../../constants/variables";
import { cloudinaryService, productService } from "../../services/modules";
import LoadingPage from "../loaders/loading-page";

const CreateProductPage = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    return () => {
      setIsLoading(false);
    };
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
      .createProduct(formData)
      .then(() => navigate(NAVIGATE_URL.PRODUCT_LIST));
  };

  return isLoading ? (
    <LoadingPage />
  ) : (
    <>
      <div className="p-6 pb-2 flex justify-center">
        <div className="text-xl text-slate-100 font-bold">
          Biểu mẫu tạo mới sản phẩm
        </div>
      </div>

      <ProductForm type={CREATE_FORM_TYPE} handleSubmitForm={onSubmitForm} />
    </>
  );
};

export default CreateProductPage;
