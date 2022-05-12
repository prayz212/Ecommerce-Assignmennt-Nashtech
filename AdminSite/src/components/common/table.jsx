import React from "react";

const Table = ({ columns, data, onRowClick = null }) => {
  if (!columns || columns.length <= 0 || !data || data.length <= 0) {
    return <></>;
  }

  return (
    <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
      <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
        <tr>
          {columns.map((column, index) => {
            return (
              <th key={index} scope="col" className="px-6 py-3 text-center">
                {column}
              </th>
            );
          })}
        </tr>
      </thead>
      <tbody>
        {data.map((item, index) => {
          const values = Object.values(item);
          return (
            <tr
              key={index}
              onClick={() => onRowClick && onRowClick(item.id)}
              className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            >
              {values.map((ele, index) => {
                return (
                  <td
                    key={index}
                    className="px-6 py-4 whitespace-normal text-center"
                  >
                    {typeof ele === "boolean" ? (ele ? "Có" : "Không") : ele}
                  </td>
                );
              })}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
};

export default Table;
