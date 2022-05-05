import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import CategoryForm from "../../components/form/category-form";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import { CREATE_FORM_TYPE } from "../../constants/variables";
import categoryService from "../../services/modules/category-service";
import LoadingPage from "../loaders/loading-page";

const CreateCategoryPage = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  const onSubmitForm = (formData) => {
    setIsLoading(true);
    
    categoryService
      .createCategory(formData)
      .then(() => {
        setIsLoading(false);
        navigate(NAVIGATE_URL.CATEGORIES_LIST)
      });
  };

  return isLoading ? (
    <LoadingPage />
  ) : (
    <>
      <div className="p-6 pb-2 flex justify-center">
        <div className="text-xl text-slate-100 font-bold text-">
          Biểu mẫu tạo mới thể loại
        </div>
      </div>

      <CategoryForm type={CREATE_FORM_TYPE} handleSubmitForm={onSubmitForm} />
    </>
  );
};

export default CreateCategoryPage;
