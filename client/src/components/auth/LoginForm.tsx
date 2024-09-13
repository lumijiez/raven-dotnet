import React from "react";
import { PiBird } from "react-icons/pi";
// import { FiUser, FiLock } from "react-icons/fi";

export const LoginForm = () => {
  return (
    <div className="flex justify-center items-center h-screen">
      <div>
        <div className="flex justify-center items-center mb-[10px]">
          <PiBird className="w-[70px] h-[70px] text-blue-base" />
        </div>
        <div className="space-y-[15px]"></div>
      </div>
    </div>
  );
};
