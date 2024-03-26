import { Form, Formik } from "formik";
import React, { useState } from "react";
import { CustomButton, FormTextInput } from "../../components/componentsIndex"
import { ResendConfirmationEmailCommand } from "../../utils/api/identity";
import { resendEmailConfirmFormValidation } from "./resendEmailConfirmFormValidationSchema";
import { resendEmailConfirmation } from "../../utils/services/identityService";

interface Props {}

const ResendEmailConfirmationPage = ({}: Props) => {
  const [isSent, setIsSent] = useState<boolean>(false);
  const resendEmailConfirmInitialValues: ResendConfirmationEmailCommand = {
    email: "",
  };

  const handleResendEmailConfirmation = async (values: ResendConfirmationEmailCommand) => {
    try {
      await resendEmailConfirmation(values);
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
      initialValues={resendEmailConfirmInitialValues}
      onSubmit={(values) => {
        handleResendEmailConfirmation(values);
      }}
      validationSchema={resendEmailConfirmFormValidation}
      validateOnChange={false}
      validateOnBlur={false}
    >
      <Form>
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", height: "700px" }}>
          <FormTextInput label="email" name="email" />
          <CustomButton text="Submit" type="submit" />
        </div>
      </Form>
    </Formik>
  );
};

export default ResendEmailConfirmationPage;
