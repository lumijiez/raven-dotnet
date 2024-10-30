// src/components/SearchBar.tsx
import React from 'react';

const SearchBar: React.FC = () => {
  return (
    <div className="flex items-start bg-white shadow-lg rounded-full w-5 lg:w-1/3 p-4 mb-4 ml-16">
        {/* Add menu icon before the search bar */}

      <svg className="w-6 h-6 text-blue-500" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-4.35-4.35m1.65-5.65A7.5 7.5 0 1112 4.5a7.5 7.5 0 016.5 6.5z"></path>
      </svg>
      <input
        type="text"
        placeholder="Search"
        className="ml-2 bg-transparent outline-none w-full text-gray-500 text-lg"
      />
    </div>
  );
};

export default SearchBar;
