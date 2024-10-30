import * as React from "react";

import { cn } from "@/lib/utils";

// eslint-disable-next-line @typescript-eslint/no-empty-object-type
export interface InputProps
  extends React.InputHTMLAttributes<HTMLInputElement> {
  icon?: React.ReactNode;
}

const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ className, type, icon, ...props }, ref) => {
    return (
      // <input
      //   type={type}
      //   className={cn(
      //     "flex h-10 w-full rounded-full border border-blue-base bg-transparent px-3 py-2 text-sm ring-offset-blue-light file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-blue-base/70 placeholder:uppercase focus-visible:outline-none focus-visible:ring-2 focus-visible:blue-base focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 dark:border-neutral-800 dark:bg-neutral-950 dark:ring-offset-neutral-950 dark:placeholder:text-neutral-400 dark:focus-visible:ring-neutral-300",
      //     className
      //   )}
      //   ref={ref}
      //   {...props}
      // />
      <div className="relative flex items-center w-full">
        {icon ? (
          <span className="absolute left-3 flex items-center text-blue-base">
            {icon}
          </span>
        ) : null}
        <input
          type={type}
          className={cn(
            "flex h-10 w-full rounded-full border border-blue-base bg-transparent px-3 py-2 text-sm ring-offset-blue-light file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-blue-base/70 placeholder:uppercase placeholder:font-light focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-neutral-900 focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 dark:border-neutral-800 dark:bg-neutral-950 dark:ring-offset-neutral-950 dark:placeholder:text-neutral-400 dark:focus-visible:ring-neutral-300",
            icon ? "pl-10" : "",
            className
          )}
          ref={ref}
          {...props}
        />
      </div>
    );
  }
);
Input.displayName = "Input";

export { Input };
