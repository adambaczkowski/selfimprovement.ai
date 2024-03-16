import { Form, Formik } from "formik";
import React from "react";
import FormTextInput from "../../components/Formik/FormTextInput/FormTextInput";
import CustomButton from "../../components/CustomButton/Button";
import { signInFormValidation } from "./signInFormValidationSchema";
import { signIn } from "../../utils/services/identityService";
import { SignInCommand } from "../../utils/api/identity";
import { Link, redirect } from "react-router-dom";

interface Props {}

const SignInPage = ({}: Props) => {
  const signInInitialValues: SignInCommand = {
    email: "",
    password: "",
  };

  const handleSignIn = async (values: SignInCommand) => {
    try {
      const response = await signIn(values);
      localStorage.setItem("userToken", JSON.stringify(response.token));
      return redirect("/");
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <Formik
      initialValues={signInInitialValues}
      onSubmit={(values: SignInCommand) => {
        handleSignIn(values);
      }}
      validationSchema={signInFormValidation}
      validateOnChange={false}
      validateOnBlur={false}
    >
      <Form>
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", height: "700px" }}>
          <h1>Sign in</h1>
          <FormTextInput label="Email" name="email" />
          <FormTextInput label="Password" name="password" />
          <CustomButton text="Sign in" type="submit" />
          <Link to={"/signUp"}>Sign up</Link>
          <Link to={"/requestPasswordReset"}>Forgot password?</Link>
        </div>
      </Form>
    </Formik>
  );
};

export default SignInPage;
