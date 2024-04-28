import { useState } from "react";
import { Link } from "react-router-dom";
import { Form, Formik } from "formik";
import { FormTextInput, FormSelectInput } from "../../components/componentsIndex"
import { ProfileCreationCommand } from "../../utils/api/identity";
import { EducationLevel } from "../../utils/enums/educationLevel";
import { profileCreationFormValidation } from "./profileCreationFormValidationSchema";
import styles from './ProfileCreationPage.module.scss';

type Props = {};

function ProfileCreationPage({}: Props) {
  const [isProfileCreationSucess, setIsProfileCreationSucess] = useState<boolean>(false);
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

  const getEnumOptions = (enumObj: any) => {
    const options: { label: string; value: string | number }[] = [];

    for (const member in enumObj) {
      if (typeof enumObj[member] === "number") {
        options.push({ label: member, value: enumObj[member] });
      }
    }
    return options;
  };
  
  const educationOptions = getEnumOptions(EducationLevel);

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
          validationSchema={null /*profileCreationFormValidation*/}
          validateOnChange={false}
          validateOnBlur={false}
        >
            <Form>
              <div className={styles.form_items_container}>
                <h1 className={styles.heading}>PROFILE CREATOR</h1>
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
                {/* <CustomButton text="Submit" type="submit" /> */}
                <Link className={styles.create_button} to={"/profileCreation"}>Create</Link>
              </div>
            </Form>

        </Formik>
      </div>
    </div>
  );
}

export default ProfileCreationPage;
