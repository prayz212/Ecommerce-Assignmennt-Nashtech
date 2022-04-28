import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import CategoryForm from "../../components/form/category-form";
import { EDIT_CATEGORY_FORM_TYPE } from "../../constants/variables";
import categoryService from "../../services/modules/category-service";

const EditCategoryPage = () => {
	const navigate = useNavigate();
  const { state } = useLocation();
  // @ts-ignore
  const { data } = state;

	const onSubmitForm = (formData) => {
		categoryService
			.updateCategory(formData)
			.then(() => navigate("/categories"));
	};

  return (
    <>
      <div className="p-6 pb-2 flex justify-center">
        <div className="text-xl text-slate-100 font-bold text-">
          Biểu mẫu cập nhật thể loại
        </div>
      </div>

      <CategoryForm type={EDIT_CATEGORY_FORM_TYPE} handleSubmitForm={onSubmitForm} item={data} />
    </>
  );
};

export default EditCategoryPage;
