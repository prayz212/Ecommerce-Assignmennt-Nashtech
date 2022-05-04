import React, { useEffect, useState } from "react";
import TopSection from "../../components/common/main-top-section";
import Table from "../../components/common/table";
import { useNavigate } from "react-router-dom";
import { DetailDialog } from "../../components/common/dialog";
import DetailCategory from "../../components/category/detail-category";
import { categoryService } from "../../services/modules";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import Pagination from "../../components/common/pagination";
import { NUMBER_RECORD_PER_PAGE } from "../../constants/variables";

const CategoryPage = () => {
  const [categories, setCategories] = useState([]);
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [openDialog, setOpenDialog] = useState(false);
  const [dialogParam, setDialogParam] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    categoryService.getCategoryList(1, NUMBER_RECORD_PER_PAGE).then(({categories, totalPage, currentPage}) => {
      setCategories(categories);
      setTotalPage(totalPage);
      setCurrentPage(currentPage);
    });
  }, []);

  const onCreateNewButtonClick = () => {
    navigate(NAVIGATE_URL.CATEGORIES_CREATE);
  };

  const onTableRowClick = (id) => {
    categoryService.getCategoryDetail(id).then((data) => {
      setOpenDialog(true);
      setDialogParam(data);
    });
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

  const onPageNumberClick = (pageNumber) => {
    categoryService
      .getCategoryList(pageNumber, NUMBER_RECORD_PER_PAGE)
      .then(({ categories, totalPage, currentPage }) => {
        setCategories(categories);
        setTotalPage(totalPage);
        setCurrentPage(currentPage);
      });
  };

  const onEditClick = (item) => {
    navigate(NAVIGATE_URL.CATEGORIES_EDIT, { state: { data: item } });
  };

  return (
    <div className="flex h-full overflow-hidden">
      <div className="w-full flex flex-col relative overflow-x-auto shadow-md sm:rounded-lg">
        <TopSection
          titleText="Danh sách thể loại"
          buttonText="Tạo mới"
          onButtonClick={onCreateNewButtonClick}
        />
        <div className="flex-1 mb-16 border border-solid border-slate-700">
          <Table
            columns={["Mã thể loại", "Tên thể loại", "Tên hiển thị"]}
            data={categories}
            onRowClick={onTableRowClick}
          />
        </div>

        {totalPage > 1 && (
          <div className="absolute bottom-0 right-0 mb-4">
            <Pagination 
              total={totalPage}
              current={currentPage}
              onClick={onPageNumberClick}
            />
          </div>
        )}
      </div>
      <DetailDialog
        isOpen={openDialog}
        onClose={onDetailDialogClose}
        component={
          <DetailCategory
            item={dialogParam}
            onDeleteClick={onDeleteClick}
            onEditClick={onEditClick}
          />
        }
      />
    </div>
  );
};

export default CategoryPage;
