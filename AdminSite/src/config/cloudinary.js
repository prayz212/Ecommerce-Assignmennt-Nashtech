var sha1 = require('sha-1');

export const CLOUDINARY_CONFIG = (image) => {
  const timestamp = Date.now();
  const public_id = image.name.split(".")[0];

  return {
    file: image,
    api_key: process.env.REACT_APP_CLOUDINARY_API_KEY,
    timestamp: timestamp,
    signature: sha1(`folder=${process.env.REACT_APP_CLOUDINARY_FOLDER}&public_id=${public_id}&timestamp=${timestamp}${process.env.REACT_APP_CLOUDINARY_API_SECRET}`),
    folder: process.env.REACT_APP_CLOUDINARY_FOLDER,
    public_id: public_id,
  }
};
