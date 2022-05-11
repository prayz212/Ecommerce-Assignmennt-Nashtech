import React from "react";

export const MasterPublicLayout = ({ component, ...rest }) => {
  return <div className="flex flex-row h-screen">{component}</div>;
};
