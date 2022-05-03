import ImageSlider from "../common/image-slider";

export const DetailProduct = ({ item, onEditClick, onDeleteClick }) => {
  return (
    item && (
      <div className="bg-white shadow sm:rounded-lg">
        <div className="px-4 py-5 sm:px-6">
          <h3 className="text-lg leading-6 font-medium text-gray-900">
            Chi tiết sản phẩm
          </h3>
          <p className="mt-1 max-w-2xl text-sm text-gray-500">
            Các thông tin chi tiết về sản phẩm
          </p>
        </div>

        <div className="border-y pb-5 border-gray-200 max-h-96 overflow-y-scroll">
          <div className="px-4 pt-5 pb-2 sm:px-6 flex flex-row">
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">Mã sản phẩm</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.id}
              </dd>
            </div>
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">
                Tên sản phẩm
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.name}
              </dd>
            </div>
          </div>

          <div className="px-4 pt-5 pb-2 sm:px-6 flex flex-row">
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">Thể loại</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.category}
              </dd>
            </div>
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">Giá bán</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.prices
                  .toString()
                  .replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")}
                đ
              </dd>
            </div>
          </div>

          <div className="px-4 pt-5 pb-2 sm:px-6 flex flex-row">
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">
                Thuộc sản phẩm đặc trưng
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.isFeatured ? "Có" : "Không"}
              </dd>
            </div>
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">
                Đánh giá trung bình
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {Math.floor(item.averageRate)}
              </dd>
            </div>
          </div>

          <div className="px-4 pt-5 pb-2 sm:px-6 flex flex-row">
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">Ngày tạo</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.createdAt}
              </dd>
            </div>
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">
                Chỉnh sửa gần nhất
              </dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.updatedAt}
              </dd>
            </div>
          </div>

          <div className="px-4 pt-5 sm:px-6 flex flex-row">
            <div className="flex-1">
              <dt className="text-sm font-medium text-gray-500">Mô tả</dt>
              <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                {item.description}
              </dd>
            </div>
          </div>

          {item.images && item.images.length > 0 && (
            <div className="px-4 py-5 sm:px-6 flex flex-row">
              <div className="flex-1">
                <dt className="text-sm font-medium text-gray-500">Hình ảnh</dt>
                <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                  <ImageSlider images={item.images} startIndex={0} />
                </dd>
              </div>
            </div>
          )}
        </div>

        <div className="flex justify-end px-4 py-3 sm:px-6">
          <button
            type="button"
            onClick={() => onEditClick(item)}
            className="text-white bg-blue-700 hover:bg-blue-800 focus:outline-none focus:ring-0 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-4 my-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            Chỉnh sửa
          </button>

          <button
            type="button"
            onClick={() => onDeleteClick(item.id)}
            className="text-white bg-red-700 hover:bg-red-800 focus:outline-none focus:ring-0 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-2 my-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900"
          >
            Xoá
          </button>
        </div>
      </div>
    )
  );
};