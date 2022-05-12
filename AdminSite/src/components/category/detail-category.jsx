import React from "react";
import CheckboxConfirm from "../common/checkbox-confirm";

const DetailCategory = ({ item, onDeleteClick, onEditClick }) => {
  return (
    item && (
      <div className="bg-white overflow-hidden sm:rounded-lg">
        <div className="px-4 py-5 sm:px-6">
          <h3 className="text-lg leading-6 font-medium text-gray-900">
            Chi tiết thể loại
          </h3>
          <p className="mt-1 max-w-2xl text-sm text-gray-500">
            Các thông tin về thể loại sản phẩm
          </p>
        </div>
        <div className="border-y border-gray-200 max-h-96 overflow-y-scroll">
          <dl>
            <div className="bg-gray-50 px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
              <dt className="text-sm font-medium text-gray-500">Mã thể loại</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.id}
              </dd>
            </div>
            <div className="bg-white px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
              <dt className="text-sm font-medium text-gray-500">
                Tên thể loại
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.name}
              </dd>
            </div>
            <div className="bg-gray-50 px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
              <dt className="text-sm font-medium text-gray-500">
                Tên hiển thị
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.displayName}
              </dd>
            </div>
            <div className="bg-white px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
              <dt className="text-sm font-medium text-gray-500">Mô tả</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.description}
              </dd>
            </div>
          </dl>
        </div>

        <CheckboxConfirm
          item={item}
          onDelete={onDeleteClick}
          onEdit={onEditClick}
        />
      </div>
    )
  );
};

export default DetailCategory;
