import { Form, Formik } from "formik";
import React, { useState } from "react";
import FormTextInput from "../../components/Formik/FormTextInput/FormTextInput";
import CustomButton from "../../components/CustomButton/Button";
import { RequestPasswordResetCommand } from "../../utils/api/identity";
import { resendEmailConfirmFormValidation } from "../ResendEmailConfirmationPage/resendEmailConfirmFormValidationSchema";
import { requestPasswordReset } from "../../utils/services/identityService";
import { Link } from "react-router-dom";

interface Props {}

export const RequestPasswordResetPage = ({}: Props) => {
  const [isSent, setIsSent] = useState<boolean>(false);
  const requestPasswordResetInitialValues: RequestPasswordResetCommand = {
    email: "",
  };

  const handleRequestPasswordReset = async (values: RequestPasswordResetCommand) => {
    try {
      await requestPasswordReset(values);
      setIsSent(true);
    } catch (err) {
      console.log(err);
    }
  };

  if (isSent) {
    return <div>SEEEEEENT</div>;
  }

  return (
    <Formik
      initialValues={requestPasswordResetInitialValues}
      onSubmit={(values) => {
        handleRequestPasswordReset(values);
      }}
      validationSchema={resendEmailConfirmFormValidation}
      validateOnChange={false}
      validateOnBlur={false}
    >
      <Form>
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", height: "700px" }}>
          <h1>Reset password</h1>
          <FormTextInput label="email" name="email" />
          <CustomButton text="Submit" type="submit" />
          <Link to={"/signUp"}>Sign up</Link>
          <Link to={"/"}>Sign in</Link>
        </div>
      </Form>
    </Formik>
  );
};
