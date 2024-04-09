import React, { useState } from "react";
import { Link } from "react-router-dom";
import { Form, Formik } from "formik";
import { CustomButton, FormTextInput, FormSelectInput } from "../../components/componentsIndex"
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
    <div className="">
        <Formik
        initialValues={creationProfileInitialValues}
        onSubmit={(values) => {
          handleSignUp(values);
        }}
        validationSchema={null /*profileCreationFormValidation*/}
        validateOnChange={false}
        validateOnBlur={false}
      >
        <div className={styles.glassContainer}>
          <Form>
            <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "10px" }}>
              <h1 style={{color: "rgba(202, 105, 105, 0.9)"}}>PROFILE CREATOR</h1>
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
              <Link to={"/profileCreation"}>Create</Link>
            </div>
          </Form>
        </div>
      </Formik>
    </div>
  );
}

export default ProfileCreationPage;
