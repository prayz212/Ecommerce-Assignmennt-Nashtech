import React, { useEffect, useState } from "react";

const CheckboxConfirm = ({ item, onDelete, onEdit }) => {
  const [confirmVisible, setConfirmVisible] = useState(false);
  const [isChecked, setIsChecked] = useState(false);
  const [isFocus, setIsFocus] = useState(false);

  useEffect(() => {
    if (isFocus) {
      setTimeout(() => setIsFocus(false), 3000);
    }
  }, [isFocus]);

  const onDeleteClicked = (id) => {
    if (!confirmVisible) setConfirmVisible(true);
    if (confirmVisible && !isChecked) setIsFocus(true);
    if (confirmVisible && isChecked) onDelete(id);
  };

  const onCheckboxChecked = () => {
    setIsChecked(!isChecked);
  };

  const renderDeleteConfirmMessage = (isVisible, isFocus) => {
    return (
      <div
        className={`flex items-center ${
          isFocus && "animate-pulse"
        } align-center ${isVisible ? "visible" : "invisible"}`}
      >
        <div className="flex items-center h-5">
          <input
            id="remember"
            type="checkbox"
            defaultChecked={isChecked}
            onChange={onCheckboxChecked}
            className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-blue-300"
            required
          />
        </div>
        <label
          htmlFor="remember"
          className={`ml-2 text-sm font-medium ${
            isFocus ? "text-rose-600" : "text-rose-500"
          } `}
        >
          Tôi xác nhận xoá thể loại này
        </label>
      </div>
    );
  };

  const renderButtons = () => {
    return (
      <div>
        <button
          type="button"
          onClick={() => onEdit(item)}
          className="text-white bg-blue-700 hover:bg-blue-800 focus:outline-none focus:ring-0 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-4 my-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          Chỉnh sửa
        </button>

        <button
          type="button"
          onClick={() => onDeleteClicked(item.id)}
          className="text-white bg-red-700 hover:bg-red-800 focus:outline-none focus:ring-0 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-2 my-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900"
        >
          Xoá
        </button>
      </div>
    );
  };

  return (
    <div className="flex justify-between px-4 py-3 sm:px-6">
      {renderDeleteConfirmMessage(confirmVisible, isFocus)}
      {renderButtons()}
    </div>
  );
};

export default CheckboxConfirm;