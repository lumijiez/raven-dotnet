import React from 'react';
import { format } from 'date-fns'; // Example with date-fns

interface ChatMessageProps {
  avatar: string;
  name: string;
  message: string;
  time: Date | number; // Use Date or timestamp for data type
  notificationCount: number;

} 

const ChatMessage: React.FC<ChatMessageProps> = ({ avatar, name, message, time, notificationCount,  }) => {

  const formattedTime = format(new Date(time), 'hh:mm a');

  return (
    <div className="relative flex items-left w-full h-full  my-[30px]">
      <div className = " flex items-center">
      <img
        className="w-16 h-16 rounded-full absolute"
        src={avatar}
        alt={name}
      />
      <div className="flex flex-col ml-20">
        <div className="text-black text-[17.38px] font-bold font-poppins leading-[26px]">
          {name}
        </div>
        <div className="text-black opacity-70 text-[15.21px] font-light font-poppins leading-[23px]">
          {message}
        </div>
      </div>
      </div>
      <div className="absolute right-10 top-0 text-black opacity-70 text-[15.21px] font-light font-poppins">
        {formattedTime} {/* Use formatted time */}
      </div>
      {notificationCount > 0 && (
        <div className="absolute right-10 top-7 flex justify-center items-center w-[22px] h-[22px] bg-[#F24E1E] rounded-full text-white text-xs">
          {notificationCount}
        </div>
      )}
    </div>
  );
};

export default ChatMessage;
