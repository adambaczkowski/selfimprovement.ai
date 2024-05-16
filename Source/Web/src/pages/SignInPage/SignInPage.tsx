import { Form, Formik } from "formik";
import { CustomButton, FormTextInput } from "../../components/componentsIndex";
import { signInFormValidation } from "./signInFormValidationSchema";
import { signIn } from "../../utils/services/identityService";
import { SignInCommand } from "../../utils/api/identity";
import { Link, redirect } from "react-router-dom";
import styles from './SignInPage.module.scss';

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
      return redirect("/tasks");
    } catch (err) {
      console.log(err);
    }
  };

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
            initialValues={signInInitialValues}
            onSubmit={(values: SignInCommand) => {
              handleSignIn(values);
            }}
            validationSchema={signInFormValidation}
            validateOnChange={false}
            validateOnBlur={false}
          >
            <Form className={styles.form_items_container}>
              <h1 className={styles.heading}>Sign in</h1>
              <FormTextInput label="Email" name="email" placeholderText="test@email.com"/>
              <FormTextInput 
                label="Password" 
                name="password" 
                type="password"
              />
              <CustomButton text="Submit" type="submit" />
              <p className={styles.or_divider}>or</p>
              <Link to={"/signUp"} className={styles.sign_up_button}>Sign up</Link>
              <Link to={"/requestPasswordReset"} className={styles.forgot_password_button}>forgot password?</Link>
            </Form>
          </Formik>
        </div>
      </div>
    </div>
  );
};

export default SignInPage;
