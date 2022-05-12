import React, { useEffect, useState } from "react";
import TopSection from "../../components/common/main-top-section";
import Pagination from "../../components/common/pagination";
import Table from "../../components/common/table";
import {
  DEFAULT_PAGE_NUMBER,
  NUMBER_RECORD_PER_PAGE,
} from "../../constants/variables";
import { accountService } from "../../services/modules";

const CustomerPage = () => {
  const [data, setData] = useState({
    clients: [],
    totalPage: 0,
    currentPage: 0,
  });

  useEffect(() => {
    accountService
      .getClients(DEFAULT_PAGE_NUMBER, 3)
      .then(({ accounts, totalPage, currentPage }) => {
        setData({
          clients: accounts,
          totalPage,
          currentPage,
        });
      });
  }, []);

  const onPageNumberClick = (pageNumber) => {
    accountService
      .getClients(pageNumber, 3)
      .then(({ accounts, totalPage, currentPage }) => {
        setData({
          clients: accounts,
          totalPage,
          currentPage,
        });
      });
  };

  return (
    <div className="flex h-full">
      <div className="w-full flex flex-col relative shadow-md sm:rounded-lg">
        <TopSection
          titleText="Danh sách khách hàng"
          buttonText=""
          buttonEnable={false}
        />
        <div className="flex-1 mb-16 border border-solid border-slate-700">
          <Table
            columns={["Mã tài khoản", "Tên tài khoản", "Email"]}
            data={data.clients}
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
    </div>
  );
};

export default CustomerPage;
