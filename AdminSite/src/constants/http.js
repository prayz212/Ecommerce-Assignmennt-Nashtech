// @ts-nocheck
export const BASE_URL = process.env.REACT_APP_ENVIRONMENT === 'development' ? process.env.REACT_APP_BASE_URL_DEV : process.env.REACT_APP_BASE_URL_PRO;
export const BASE_API_URL = process.env.REACT_APP_BASE_API_URL_DEV || "";
export const BASE_CLOUDINARY_URL = process.env.REACT_APP_CLOUDINARY_API_URL;

export const HTTP_HEADER = {
  "Accept": "application/json",
  "Content-Type": "application/json",
  'X-Requested-With': 'XMLHttpRequest',
}