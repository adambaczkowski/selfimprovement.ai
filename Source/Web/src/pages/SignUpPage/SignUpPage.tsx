import { Form, Formik } from "formik";
import React, { useState } from "react";
import FormTextInput from "../../components/Formik/FormTextInput/FormTextInput";
import CustomButton from "../../components/CustomButton/Button";
import { SignUpCommand } from "../../utils/api/identity";
import { signUpFormValidation } from "./signUpFormValidationSchema";
import { signUp } from "../../utils/services/identityService";
import { Link } from "react-router-dom";

interface Props {}

const SignUpPage = ({}: Props) => {
  const [isSignUpSucess, setIsSignUpSucess] = useState<boolean>(false);
  const signUpInitialValues: SignUpCommand = {
    email: "",
    password: "",
    confirmPassword: "",
  };

  const handleSignUp = async (values: SignUpCommand) => {
    try {
      const response = await signUp(values);
      if (response.isSuccess) {
        setIsSignUpSucess(true);
      }
    } catch (err) {
      console.log(err);
    }
  };

  if (isSignUpSucess) {
    return <div>Yea good!</div>;
  }

  return (
    <Formik
      initialValues={signUpInitialValues}
      onSubmit={(values) => {
        handleSignUp(values);
      }}
      validationSchema={null /*signUpFormValidation*/}
      validateOnChange={false}
      validateOnBlur={false}
    >
      <Form>
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", height: "700px" }}>
          <h1>Sign up</h1>
          <FormTextInput label="Email" name="email" />
          <FormTextInput label="Password" name="password" type="password" />
          <FormTextInput label="Confirm Password" name="confirmPassword" type="password" />
          <CustomButton text="Submit" type="submit" />
          <Link to={"/"}>Sign in</Link>
          <Link to={"/requestPasswordReset"}>Forgot password?</Link>
        </div>
      </Form>
    </Formik>
  );
};

export default SignUpPage;
