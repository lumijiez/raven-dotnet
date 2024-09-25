import ChatMessage from "./ChatMessage";
import { ScrollArea } from "@/components/ui/scroll-area"

const ContactsSidebar = () => {
  return (
    <ScrollArea className="bg-[#E5F3FA] p-4 rounded-2xl h-[450px]"> {/* Set height here */}
      
      <ChatMessage 
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
      <ChatMessage
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={2}
      />
      <ChatMessage
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1} 
      />
      <ChatMessage
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
      <ChatMessage
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1} 
      />
      <ChatMessage
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
      <ChatMessage 
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
       <ChatMessage 
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
       <ChatMessage 
       avatar="https://via.placeholder.com/61x60"
       name="Cameron Williamson"
       message="omg, this is amazing"
       time={Date.now()} // Timestamp (current time in milliseconds)
       notificationCount={1}
      />
    </ScrollArea>
  );
};

export default ContactsSidebar;
