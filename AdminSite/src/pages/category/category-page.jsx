import React, { useEffect, useState } from "react";
import TopSection from "../../components/common/main-top-section";
import Table from "../../components/common/table";
import { useNavigate } from "react-router-dom";
import { DetailDialog } from "../../components/common/dialog";
import DetailCategory from "../../components/category/detail-category";
import { categoryService } from "../../services/modules";
import { NAVIGATE_URL } from "../../constants/navigate-url";
import Pagination from "../../components/common/pagination";
import { DEFAULT_PAGE_NUMBER, NUMBER_RECORD_PER_PAGE } from "../../constants/variables";
import LoadingPage from "../loaders/loading-page";

const CategoryPage = () => {
  const [data, setData] = useState({ categories: [], totalPage: 0, currentPage: 0 });
  const [openDialog, setOpenDialog] = useState(false);
  const [dialogParam, setDialogParam] = useState({});
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    return () => {
      setIsLoading(false);
    };
  }, []);

  useEffect(() => {
    categoryService
      .getCategories(DEFAULT_PAGE_NUMBER, NUMBER_RECORD_PER_PAGE)
      .then((response) => {
        setData(response);
      });
  }, []);

  const onCreateNewButtonClick = () => {
    navigate(NAVIGATE_URL.CATEGORIES_CREATE);
  };

  const onTableRowClick = (id) => {
    categoryService
      .getCategoryDetail(id)
      .then((data) => {
        setOpenDialog(true);
        setDialogParam(data)
      });
  };

  const onDetailDialogClose = () => {
    setOpenDialog(false);
  };

  const onDeleteClick = (id) => {
    setOpenDialog(false);
    setIsLoading(true);

    categoryService
      .deleteCategory(id)
      .then((result) => {
        if (result) {
          const newCategories = data.categories.filter(
            (category) => category.id !== id
          );
          setData({...data, categories: newCategories});
        }
        return result;
      })
      .finally(() => setIsLoading(false));
  };

  const onPageNumberClick = (pageNumber) => {
    categoryService
      .getCategories(pageNumber, NUMBER_RECORD_PER_PAGE)
      .then((response) => {
        setData(response)
      });
  };

  const onEditClick = (item) => {
    navigate(NAVIGATE_URL.CATEGORIES_EDIT, {state: {data: item}});
  };

  return isLoading ? (
    <LoadingPage />
  ) : (
    <div className="flex h-full">
      <div className="w-full flex flex-col relative shadow-md sm:rounded-lg">
        <TopSection
          titleText="Danh s??ch th??? lo???i"
          buttonText="T???o m???i"
          onButtonClick={onCreateNewButtonClick}
        />
        <div className="flex-1 mb-16 border border-solid border-slate-700">
          <Table
            columns={["M?? th??? lo???i", "T??n th??? lo???i", "T??n hi???n th???"]}
            data={data.categories}
            onRowClick={onTableRowClick}
          />
        </div>

        {data.totalPage > 1 && (
          <div className="absolute bottom-0 right-0 mb-4">
            <Pagination
              total={data.totalPage}
              current={data.currentPage}
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
