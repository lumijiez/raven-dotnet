import * as React from "react";
// import { IoMenu } from "react-icons/io5";
import { FiMenu } from "react-icons/fi";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Switch } from "@/components/ui/switch"; // Make sure the path is correct

const MoreMenu = () => {
  const handleClick = (event: React.MouseEvent) => {
    // Stop the event from propagating to avoid closing the dropdown
    event.stopPropagation();
  };

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="focus-visible:outline-none">
          <FiMenu className="w-[30px] h-[30px] text-blue-base" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-56">
        <DropdownMenuLabel>My Account</DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuGroup>
          <DropdownMenuItem>Create new group</DropdownMenuItem>
          <DropdownMenuItem>Contacts</DropdownMenuItem>
          <DropdownMenuItem>Calls</DropdownMenuItem>
          <DropdownMenuItem>
            <div
              className="flex items-center justify-between w-full"
              onClick={handleClick}
            >
              Night Mode
              <Switch />
            </div>
          </DropdownMenuItem>
          <DropdownMenuItem>Settings</DropdownMenuItem>
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default MoreMenu;
