// src/App.tsx
import ContactsSidebar from "./components/chat/ContactsSidebar";
import MoreMenu from "./components/MoreMenu";
//import SearchBar from "./components/SearchBar"; // Import the SearchBar component

function App() {
  return (
    <>
      <div className="p-4">
        <MoreMenu/>
         <ContactsSidebar/>
      </div>
    </>
  );
}

export default App;
