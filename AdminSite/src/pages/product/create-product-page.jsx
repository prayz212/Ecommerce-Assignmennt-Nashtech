import React from "react";
import { useNavigate } from "react-router-dom";
import ProductForm from "../../components/form/product-form";
import { CREATE_FORM_TYPE } from "../../constants/variables";

const CreateProductPage = () => {
  const navigate = useNavigate();

  const onSubmitForm = (formData) => {
    console.log("submited form");
    console.log(formData);
  }

  return (
    <>
      <div className="p-6 pb-2 flex justify-center">
        <div className="text-xl text-slate-100 font-bold text-">
          Biểu mẫu tạo mới sản phẩm
        </div>
      </div>

      <ProductForm
        type={CREATE_FORM_TYPE}
        handleSubmitForm={onSubmitForm}
      />
    </>
  );
};

export default CreateProductPage;
