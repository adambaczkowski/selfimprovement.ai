import { useState } from "react";
import { useParams, Link } from "react-router-dom";
import { Form, Formik } from "formik";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { FormTextInput, FormSelectInput } from "../../components/componentsIndex"
import { ProfileCreationCommand } from "../../utils/api/identity";
import { EducationLevel, educationOptions } from "../../utils/enums/educationLevel";
import { ProfileCreationFormValidation } from "./ProfileCreationFormValidation";
import styles from './ProfileCreationPage.module.scss';

type Props = {};

// This page has two modes: edit and create. The mode is determined by the URL parameter.
// Edit Profile URL example: /profileCreation/edit
// New Profile URL example: /profileCreation/new
function ProfileCreationPage({}: Props) {
  const [isProfileCreationSucess, setIsProfileCreationSucess] = useState<boolean>(false);
  const { mode } = useParams<{ mode: string }>();

  const creationProfileInitialValues: ProfileCreationCommand = {
    name: "",
    surname: "",
    weight: null,
    height: null,
    age: null,
    educationLevel: EducationLevel.Primary,
    profileImage: null,
  };
  

  const handleSignUp = async (values: ProfileCreationCommand) => {
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
  if (mode !== "new" && mode !== "edit") {
    return <div>Invalid URL</div>;
  }

  let pageTitle: string;
  if (mode === "edit") {
    pageTitle = "EDIT PROFILE";
  } else if (mode === "new") {
    pageTitle = "NEW PROFILE";
  } else {
    pageTitle = "DEFAULT PROFILE";
  }

  const goBackButton = mode !== "new" ? (
    <Link className={styles.go_back_button} to='/tasks'>
      <ArrowBackIcon />
    </Link>
  ) : null;

  return (
    <div className={styles.background_container}>
      {goBackButton}
      <div className={styles.extended_background_container}>
        <Formik
          initialValues={creationProfileInitialValues}
          onSubmit={(values) => {
            handleSignUp(values);
          }}
          validationSchema={ProfileCreationFormValidation}
          validateOnChange={false}
          validateOnBlur={false}
        >
            <Form>
              <div className={styles.form_items_container}>
                <h1 className={styles.heading}>{pageTitle}</h1>
                <FormTextInput label="Name" name="name" />
                <FormTextInput label="Surname" name="surname" />
                <FormTextInput label="Weight" name="weight" />
                <FormTextInput label="Height" name="height" />
                <FormTextInput label="Age" name="age" />
                <FormSelectInput
                  label="Education Level"
                  name="educationLevel"
                  value={EducationLevel.Primary}
                  options={educationOptions}
                />
                <button className={styles.create_button}>Create</button>
              </div>
            </Form>

        </Formik>
      </div>
    </div>
  );
}

export default ProfileCreationPage;
