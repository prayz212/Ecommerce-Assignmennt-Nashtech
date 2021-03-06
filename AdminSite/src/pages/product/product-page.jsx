import { useEffect, useState } from "react";
import TopSection from "../../components/common/main-top-section";
import Pagination from "../../components/common/pagination";
import Table from "../../components/common/table";
import { productService } from "../../services/modules";
import { DEFAULT_PAGE_NUMBER, NUMBER_RECORD_PER_PAGE } from "../../constants/variables.js";
import { DetailDialog } from "../../components/common/dialog";
import { DetailProduct } from "../../components/product/detail-product";
import { useNavigate } from "react-router-dom";
import { NAVIGATE_URL } from "../../constants/navigate-url.js";
import _ from "lodash";

const ProductPage = () => {
  const [data, setData] = useState({ products: [], totalPage: 0, currentPage: 0 });
  const [openDialog, setOpenDialog] = useState(false);
  const [dialogParam, setDialogParam] = useState({});

  const navigate = useNavigate();

  useEffect(() => {
    productService
      .getProductList(DEFAULT_PAGE_NUMBER, NUMBER_RECORD_PER_PAGE)
      .then((response) => {
        setData(response);
      });
  }, []);

  useEffect(() => {
    if (!_.isEmpty(dialogParam)) {
      setOpenDialog(true);
    }
  }, [dialogParam]);

  const onCreateNewButtonClick = () => {
    navigate(NAVIGATE_URL.PRODUCT_CREATE);
  };

  const onTableRowClick = (id) => {
    productService.getProductDetail(id).then((data) => {
      setDialogParam(data);
    });
  };

  const onPageNumberClick = (pageNumber) => {
    productService
      .getProductList(pageNumber, NUMBER_RECORD_PER_PAGE)
      .then((response) => {
        setData(response);
      });
  };

  const onEditClick = (item) => {
    navigate(NAVIGATE_URL.PRODUCT_EDIT, { state: { data: item } });
  };

  const onDeleteClick = (id) => {
    productService.deleteProduct(id).then((result) => {
      if (result) {
        const newProducts = data.products.filter((product) => product.id !== id);
        setData({...data, products: newProducts});
        setOpenDialog(false);
      }
    });
  };

  return (
    <div className="flex h-full">
      <div className="w-full flex flex-col relative shadow-md sm:rounded-lg">
        <TopSection
          titleText="Danh s??ch s???n ph???m"
          buttonText="T???o m???i"
          onButtonClick={onCreateNewButtonClick}
        />
        <div className="flex-1 mb-16 border border-solid border-slate-700">
          <Table
            columns={[
              "M?? s???n ph???m",
              "T??n s???n ph???m",
              "Gi?? ti???n",
              "?????c tr??ng",
              "Th??? lo???i",
            ]}
            data={data.products}
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
        onClose={() => setOpenDialog(false)}
        component={
          <DetailProduct
            item={dialogParam}
            onEditClick={onEditClick}
            onDeleteClick={onDeleteClick}
          />
        }
      />
    </div>
  );
};

export default ProductPage;
