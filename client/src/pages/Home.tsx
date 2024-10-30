// src/pages/Home.tsx

//import React from "react";
import MoreMenu from "../components/MoreMenu";
import SearchBar from "../components/SearchBar";
import ContactsSidebar from "@/components/chat/ContactsSidebar";
import { CardsChat } from "@/components/chat/CardsChat";

const Home = () => {
  return (
    <div style = {{height: 100+"vh"}}>
      {/* Header */}
      <header className="flex items-center p-4">
        <MoreMenu />
        <SearchBar />
      </header>

      {/* Main Content */}
      <div className="flex flex-1">
        {/* Contacts Sidebar */}
        <div className="hidden lg:block lg:w-1/3 p-4">
          <ContactsSidebar />
        </div>

        {/* Cards Chat */}
        <div className="flex-1 p-4">
          <CardsChat />
        </div>
      </div>
    </div>
  );
};

export default Home;
