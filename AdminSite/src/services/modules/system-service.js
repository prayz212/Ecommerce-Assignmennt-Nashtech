import { setRefreshToken, getRefreshToken as _getRefreshToken, removeRefreshToken } from "../utils/local-storage";

const SECRET_JWT_FLAG = process.env.REACT_APP_SECRET_JWT_FLAG;

/* 
  REFRESH TOKEN look like:
  {
    token: <Token>,
    expiredTime: <Time>
  }
*/
const saveRefreshToken = (tokenObject) => {
  const newToken = {
    ...tokenObject,
    token: SECRET_JWT_FLAG + "|" + tokenObject.token
  };

  if (getRefreshToken()) {
    removeRefreshToken();
  }

  setRefreshToken(newToken);
};


/* 
  REFRESH TOKEN look like:
  {
    token: <Token>,
    expiredTime: <Time>
  }
*/
const getRefreshToken = () => {
  const tokenObject = _getRefreshToken();
  if (!tokenObject) return null;

  const {token, expiredTime} = tokenObject;
  if (Date.parse(expiredTime) < Date.now()) return null;
  
  const [secretFlag, refreshToken] = token.split('|');
  return secretFlag === SECRET_JWT_FLAG ? refreshToken : null;
}

const deleteRefreshToken = () => {
  removeRefreshToken();
};

export default {
  saveRefreshToken,
  getRefreshToken,
  deleteRefreshToken,
}