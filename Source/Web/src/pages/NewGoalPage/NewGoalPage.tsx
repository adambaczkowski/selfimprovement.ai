import { useState } from "react";
import { Link } from "react-router-dom";
import { Form, Formik } from "formik";
import { FormTextInput, FormSelectInput } from "../../components/componentsIndex"
import { GoalCreationCommand } from "../../utils/api/identity";
import { EducationLevel } from "../../utils/enums/educationLevel";
import { NewGoalFormValidation } from "./NewGoalFormValidationSchema";
import styles from './NewGoalPage.module.scss';

type Props = {};

function NewGoalPage({}: Props) {
  const [isProfileCreationSucess, setIsProfileCreationSucess] = useState<boolean>(false);
  const creationProfileInitialValues: GoalCreationCommand = {
    category: "",
    timeAvailability: "",
    duration: null,
    experience: "",
    learningType: "",
  };

  const handleSignUp = async (values: GoalCreationCommand) => {
    console.log(values);
    // try {
    //   const response = await signUp(values);
    //   if (response.isSuccess) {
    //     setIsSignUpSucess(true);
    //   }
    // } catch (err) {
    //   console.log(err);
    // }
  };

  if (isProfileCreationSucess) {
    return <div>Yea good!</div>;
  }

  return (
    <div className={styles.background_container}>
      <div className={styles.extended_background_container}>
        <Formik
          initialValues={creationProfileInitialValues}
          onSubmit={(values) => {
            handleSignUp(values);
          }}
          validationSchema={NewGoalFormValidation}
          validateOnChange={false}
          validateOnBlur={false}
        >
          <Form className={styles.form_items_container}>
            <h1 className={styles.heading}>New Goal</h1>
            <FormTextInput label="Category" name="category" />
            <FormTextInput label="Time Availability" name="timeAvailability" />
            <FormTextInput label="Duration" name="duration" />
            <FormTextInput label="Experience" name="experience" />
            <FormTextInput label="Learning Type" name="learningType" />
            <button className={styles.create_button}>Create</button>
          </Form>
        </Formik>
      </div>
    </div>
  );
}

export default NewGoalPage;
