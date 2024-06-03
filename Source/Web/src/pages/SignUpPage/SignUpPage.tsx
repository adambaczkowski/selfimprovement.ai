import { Form, Formik } from "formik";
import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { CustomButton, FormTextInput, LoadingCircle } from "../../components/componentsIndex"
import { SignUpCommand } from "../../utils/api/identity";
import { signUpFormValidation } from "./signUpFormValidationSchema";
import { signUp } from "../../utils/services/identityService";
import { Link } from "react-router-dom";
import styles from './SignUpPage.module.scss';

interface Props {}

const SignUpPage = ({}: Props) => {
  const navigate = useNavigate();
  const [isSignUpSucess, setIsSignUpSucess] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState(false);
  const signUpInitialValues: SignUpCommand = {
    email: "",
    name: "",
    password: "",
    confirmPassword: "",
  };

  const handleSignUp = async (values: SignUpCommand) => {
    setIsLoading(true);
    try {
      const response = await signUp(values);
      if (response.isSuccess) {
        setIsSignUpSucess(true);
      }
      navigate(`/signIn`);
    } catch (err) {
      console.log(err);
    } finally {
      setIsLoading(false);
    }
  };

  if (isSignUpSucess) {
    return <div>Yea good!</div>;
  }

  return (
    <div className={styles.background_container}>
      {isLoading ? (
        <LoadingCircle timeout={10000000} heading='Signing up...' errorMessage="Something went wrong" />
      ) : (
        <div className={styles.login_grid}>
          <img
            src="https://epe.brightspotcdn.com/40/fe/ca6fa2e84f8fa4117006fe3c88d6/mindfulness-opinion-1395895552-b-ly.jpg" 
            alt="login" 
            className={styles.login_image}
          />
          <div>
            <h1 className={styles.login_heading}>selfimprovement.io</h1>
            <Formik
              initialValues={signUpInitialValues}
              onSubmit={(values) => {
                handleSignUp(values);
              }}
              validationSchema={signUpFormValidation}
              validateOnChange={false}
              validateOnBlur={false}
            >
              <Form className={styles.form_items_container}>
                <h1 className={styles.heading}>Sign up</h1>
                <FormTextInput label="Email" name="email" placeholderText="test@email.com"/>
                <FormTextInput label="Name" name="name" placeholderText="Max"/>
                <FormTextInput label="Password" name="password" type="password" />
                <FormTextInput label="Confirm Password" name="confirmPassword" type="password" />
                <CustomButton text="Submit" type="submit" />
                <p className={styles.or_divider}>or</p>
                <Link to={"/"} className={styles.sign_up_button}>Sign in</Link>
                <Link to={"/requestPasswordReset"} className={styles.forgot_password_button}>forgot password?</Link>
              </Form>
            </Formik>
          </div>
        </div>
      )}
    </div>
  );
};

export default SignUpPage;
