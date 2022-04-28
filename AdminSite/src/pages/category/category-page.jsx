import React, { useEffect, useState } from "react";
import TopSection from "../../components/common/main-top-section";
import Table from "../../components/common/table";
import categoryService from "../../services/modules/category-service";
import { useNavigate } from "react-router-dom";
import { DetailDialog } from "../../components/common/dialog";
import DetailCategory from "../../components/category/detail-category";

const CategoryPage = () => {
  const [categories, setCategories] = useState([]);
  const [openDialog, setOpenDialog] = useState(false);
  const [dialogParam, setDialogParam] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    categoryService
      .getCategoryList()
      // @ts-ignore
      .then((data) => setCategories(data));
  }, []);

  const onCreateNewButtonClick = () => {
    navigate("/categories/create");
  };

  const onTableRowClick = (id) => {
    setOpenDialog(true);
    categoryService.getCategoryDetail(id).then((data) => setDialogParam(data));
  };

  const onDetailDialogClose = () => {
    setOpenDialog(false);
  };

  const onDeleteClick = (id) => {
    categoryService.deleteCategory(id).then((result) => {
      if (result) {
        const newCategories = categories.filter(
          (category) => category.id !== id
        );
        setCategories(newCategories);
        setOpenDialog(false);
      }

      return result;
    });
  };

  const onEditClick = (item) => {
    navigate("/categories/edit", {state: {data: item}});
  };

  return (
    <div className="flex h-full overflow-hidden">
      <div className="w-full relative overflow-x-auto shadow-md sm:rounded-lg">
        <TopSection
          titleText="Danh sách thể loại"
          buttonText="Tạo mới"
          onButtonClick={onCreateNewButtonClick}
        />
        <Table
          columns={["Mã thể loại", "Tên thể loại", "Tên hiển thị"]}
          data={categories}
          onRowClick={onTableRowClick}
        />
      </div>
      <DetailDialog
        isOpen={openDialog}
        onClose={onDetailDialogClose}
        component={
          <DetailCategory item={dialogParam} onDeleteClick={onDeleteClick} onEditClick={onEditClick} />
        }
      />
    </div>
  );
};

export default CategoryPage;
