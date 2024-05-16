import { Form, Formik } from "formik";
import { useState } from "react";
import { CustomButton, FormTextInput } from "../../components/componentsIndex"
import { RequestPasswordResetCommand } from "../../utils/api/identity";
import { resendEmailConfirmFormValidation } from "../ResendEmailConfirmationPage/resendEmailConfirmFormValidationSchema";
import { requestPasswordReset } from "../../utils/services/identityService";
import { Link } from "react-router-dom";
import styles from './RequestPasswordResetPage.module.scss';

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
    <div className={styles.background_container}>
      <div className={styles.login_grid}>
        <img
          src="https://epe.brightspotcdn.com/40/fe/ca6fa2e84f8fa4117006fe3c88d6/mindfulness-opinion-1395895552-b-ly.jpg" 
          alt="login" 
          className={styles.login_image}  
        />
        <div>
          <h1 className={styles.login_heading}>selfimprovement.io</h1>
          <Formik
            initialValues={requestPasswordResetInitialValues}
            onSubmit={(values) => {
              handleRequestPasswordReset(values);
            }}
            validationSchema={resendEmailConfirmFormValidation}
            validateOnChange={false}
            validateOnBlur={false}
          >
            <Form className={styles.form_items_container}>
              <h1 className={styles.heading}>Reset password</h1>
              <FormTextInput label="email" name="email" placeholderText="test@email.com" />
              <CustomButton text="Submit" type="submit" />
              <p className={styles.or_divider}>or</p>
              <Link to={"/signUp"} className={styles.sign_up_button}>Sign up</Link>
              <Link to={"/"} className={styles.forgot_password_button}>Sign in</Link>
            </Form>
          </Formik>
        </div>
      </div>
    </div>
  );
};
