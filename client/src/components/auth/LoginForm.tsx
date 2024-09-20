import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

import { FiUser, FiLock } from "react-icons/fi";
import { PiBird } from "react-icons/pi";

import { useLoginUserMutation } from "../../features/auth/authAPI";

// type LoginFormInputs = {
//   username: string;
//   password: string;
// };

export const LoginForm = () => {
  const [loginUser] = useLoginUserMutation();

  const loginFormSchema = z.object({
    username: z
      .string()
      .min(2, { message: "Username must be at least 2 characters long" })
      .max(50, {
        message: "Username must be no more than 10 characters long.",
      }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long" }) // minimum length of 8 characters
      .max(100, {
        message: "Password must be no more than 100 characters long",
      })
      .regex(/[a-z]/, {
        message: "Password must contain at least one lowercase letter",
      })
      .regex(/[A-Z]/, {
        message: "Password must contain at least one uppercase letter",
      })
      .regex(/\d/, { message: "Password must contain at least one number" })
      .regex(/[@$!%*?&#]/, {
        message: "Password must contain at least one special character",
      }),
  });

  const loginForm = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  //TO DO: implement onsubmit logic
  // const onSubmit = (values: z.infer<typeof loginFormSchema>) => {
  //   alert(JSON.stringify(values, null, 2));

  //   loginForm.reset();
  // };
  const onSubmit = async (data: z.infer<typeof loginFormSchema>) => {
    try {
      const response = await loginUser(data).unwrap();
      console.log("Login successful:", response);
      // Handle success, like saving the token or redirecting
    } catch (err) {
      console.error("Failed to login:", err);
    }
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <div className="w-[400px]">
        <div className="flex justify-center items-center mb-[10px]">
          <PiBird className="w-[70px] h-[70px] text-blue-base" />
        </div>
        <div>
          <Form {...loginForm}>
            <form
              onSubmit={loginForm.handleSubmit(onSubmit)}
              className="space-y-[12px]"
            >
              <FormField
                control={loginForm.control}
                name="username"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiUser />}
                        placeholder="Username"
                        {...field}
                        {...loginForm.register("username")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={loginForm.control}
                name="password"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiLock />}
                        type="password"
                        placeholder="Password"
                        {...field}
                        {...loginForm.register("password")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button className="w-full" type="submit">
                Log in
              </Button>
            </form>
          </Form>
        </div>
      </div>
    </div>
  );
};
