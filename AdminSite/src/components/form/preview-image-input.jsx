import React from "react";

const PreviewImageInput = ({ images, onImageDelete }) => {
  console.log(images);
  return (
    <div className="flex-1">
      <h1 className="pb-3 font-medium sm:text-base text-gray-500">
        Ảnh đã chọn
      </h1>

      {images.length <= 0 && (
        <ul id="gallery" className="flex flex-1 flex-wrap -m-1">
          <li
            id="empty"
            className="h-full w-full text-center flex flex-col items-center justify-center items-center"
          >
            <img
              className="mx-auto w-32"
              src="https://user-images.githubusercontent.com/507615/54591670-ac0a0180-4a65-11e9-846c-e55ffce0fe7b.png"
              alt="no data"
            />
            <span className="text-small text-gray-500">Chưa chọn ảnh nào</span>
          </li>
        </ul>
      )}

      {images.length > 0 && (
        <ul className="flex">
          {images.map((image) => (
            <li
              key={image.name}
              className="block p-1 w-1/2 sm:w-1/3 md:w-1/4 lg:w-1/6 xl:w-1/8 h-24"
            >
              <article
                tabIndex={0}
                className="group w-full h-full rounded-md focus:outline-none focus:shadow-outline elative bg-gray-100 cursor-pointer relative shadow-sm"
              >
                <img
                  src={
                    image instanceof File
                      ? URL.createObjectURL(image)
                      : image.uri
                  }
                  alt="upload preview"
                  className="img-preview w-full h-full sticky object-cover rounded-md bg-fixed"
                />

                <section className="flex flex-col invisible group-hover:visible rounded-md text-xs break-words w-full h-full z-20 absolute top-0 py-2 px-3">
                  <h1 className="flex-1 group-hover:text-black truncate overflow-hidden font-normal">
                    {image instanceof File
                      ? image.name.split(".").slice(0, -1).join(".")
                      : image.name}
                  </h1>
                  <div className="flex">
                    <p className="p-1 size text-xs text-black">
                      {image.size > 1024
                        ? image.size > 1048576
                          ? Math.round(image.size / 1048576) + "Mb"
                          : Math.round(image.size / 1024) + "Kb"
                        : image.size + "B"}
                    </p>
                    <button
                      type="button"
                      className="ml-auto focus:outline-none hover:bg-gray-300 p-1 rounded-md text-gray-800"
                      onClick={() => onImageDelete(image)}
                    >
                      <svg
                        className="pointer-events-none fill-current w-4 h-4 ml-auto"
                        xmlns="http://www.w3.org/2000/svg"
                        width="24"
                        height="24"
                        viewBox="0 0 24 24"
                      >
                        <path
                          className="pointer-events-none"
                          d="M3 6l3 18h12l3-18h-18zm19-4v2h-20v-2h5.711c.9 0 1.631-1.099 1.631-2h5.316c0 .901.73 2 1.631 2h5.711z"
                        />
                      </svg>
                    </button>
                  </div>
                </section>
              </article>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default PreviewImageInput;
