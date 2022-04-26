import {REFRESH_TOKEN_KEY} from '../../constants/local-storage'

export const setRefreshToken = (refreshToken) => localStorage.setItem(REFRESH_TOKEN_KEY, JSON.stringify(refreshToken));

export const getRefreshToken = () => JSON.parse(localStorage.getItem(REFRESH_TOKEN_KEY)) || null;

export const removeRefreshToken = () => localStorage.removeItem(REFRESH_TOKEN_KEY);