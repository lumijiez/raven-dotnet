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

import { FiUser, FiLock, FiMail } from "react-icons/fi";
import { PiBird } from "react-icons/pi";
import { useRegisterUserMutation } from "@/features/auth/authAPI";

export const RegisterForm = () => {
  const [registerUser] = useRegisterUserMutation();

  const registerFormSchema = z
    .object({
      username: z
        .string()
        .min(2, { message: "Username must be at least 2 characters long" })
        .max(20, {
          message: "Username must be no more than 20 characters long.",
        }),
      email: z.string().email({ message: "Invalid email address" }),
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
      confirmPassword: z.string(),
    })
    .refine((data) => data.password === data.confirmPassword, {
      path: ["confirmPassword"],
      message: "Passwords do not match.",
    });

  const registerForm = useForm<z.infer<typeof registerFormSchema>>({
    resolver: zodResolver(registerFormSchema),
    defaultValues: {
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
    },
  });

  //TO DO: implement onsubmit logic
  // const onSubmit = (values: z.infer<typeof registerFormSchema>) => {
  //   alert(JSON.stringify(values, null, 2));

  //   registerForm.reset();
  // };

  const onSubmit = async (data: z.infer<typeof registerFormSchema>) => {
    if (data.password !== data.confirmPassword) {
      console.error("Passwords do not match");
      return;
    }

    try {
      await registerUser({
        username: data.username,
        email: data.email,
        password: data.password,
      }).unwrap();
      console.log("Registration successful");
      // Handle success like redirecting
    } catch (err) {
      console.error("Failed to register:", err);
    }
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <div className="w-[400px]">
        <div className="flex justify-center items-center mb-[10px]">
          <PiBird className="w-[70px] h-[70px] text-blue-base" />
        </div>
        <div>
          <Form {...registerForm}>
            <form
              onSubmit={registerForm.handleSubmit(onSubmit)}
              className="space-y-[12px]"
            >
              <FormField
                control={registerForm.control}
                name="username"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiUser />}
                        placeholder="Username"
                        {...field}
                        {...registerForm.register("username")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={registerForm.control}
                name="email"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiMail />}
                        placeholder="Email"
                        {...field}
                        {...registerForm.register("email")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={registerForm.control}
                name="password"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiLock />}
                        type="password"
                        placeholder="Password"
                        {...field}
                        {...registerForm.register("password")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={registerForm.control}
                name="confirmPassword"
                render={({ field }) => (
                  <FormItem>
                    <FormControl>
                      <Input
                        icon={<FiLock />}
                        type="password"
                        placeholder="Confirm Password"
                        {...field}
                        {...registerForm.register("confirmPassword")}
                        required
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button className="w-full" type="submit">
                Sign Up
              </Button>
            </form>
          </Form>
        </div>
      </div>
    </div>
  );
};
