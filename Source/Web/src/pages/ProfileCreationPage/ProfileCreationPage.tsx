import { useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { useQuery } from "react-query";
import { Form, Formik } from "formik";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { FormTextInput, FormSelectInput, FormImageInput } from "../../components/componentsIndex";
import { ProfileCreationFormValidation } from "./ProfileCreationFormValidation";
import styles from "./ProfileCreationPage.module.scss";
import { Sex, Education, UserProfileDto } from "../../utils/api/identity";
import { createUser, editUser, fetchUser } from "../../utils/services/userService";
import { enumToArrayOfOptions } from "../../utils/helpers/enumToArrayOfOptions";

// This page has two modes: edit and create. The mode is determined by the URL parameter.
// Edit Profile URL example: /profileCreation/edit
// New Profile URL example: /profileCreation/new
function ProfileCreationPage() {
  const [isLoading, setIsLoading] = useState(false);
  const [user, setUser] = useState<UserProfileDto | null>(null);
  const navigate = useNavigate();
  const [isProfileCreationSuccess, setIsProfileCreationSuccess] = useState<boolean>(false);
  const { mode } = useParams<{ mode: string }>();

  useQuery({
    queryKey: ["getUser"],
    queryFn: async () => {
      const user = await fetchUser();
      if (user) {
        setUser(user);
      }
      return user;
    },
    refetchOnWindowFocus: false,
  });

  
  const creationProfileInitialValues: UserProfileDto = {
    sex: undefined,
    weight: null,
    height: null,
    age: null,
    educationLevel: undefined,
    profileImageData: null,
  };

  const handleCreateProfile = async (values: UserProfileDto) => {
    setIsLoading(true);
    try {
      await createUser(values); 
      setIsProfileCreationSuccess(true);
      navigate(`/`);
      window.location.reload();
    } catch (error) {
      console.error('Error editing profile:', error);
      // Handle error if needed
    } finally {
      setIsLoading(false);
    }
  };

  const handleEditProfile = async (values: UserProfileDto) => {
    setIsLoading(true);
    try {
      await editUser(values); 
      setIsProfileCreationSuccess(true);
      navigate(`/`);
    } catch (error) {
      console.error('Error editing profile:', error);
      // Handle error if needed
    } finally {
      setIsLoading(false);
    }
  };

  if (isProfileCreationSuccess) {
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
      <Link className={styles.go_back_button} to="/tasks">
        <ArrowBackIcon />
      </Link>
    ) : null;

  return (
    <div className={styles.background_container}>
      {mode === "edit" ? goBackButton : null}
      <div className={styles.extended_background_container}>
        <Formik
          initialValues={creationProfileInitialValues}
          onSubmit={handleCreateProfile}
          validationSchema={null} //NewGoalFormValidation}
          validateOnChange={false}
          validateOnBlur={false}
        >
          <Form className={styles.form_items_container}>
            <h1 className={styles.heading}>{pageTitle}</h1>
            <FormTextInput label="Weight" name="weight" />
            <FormTextInput label="Height" name="height" />
            <FormTextInput label="Age" name="age" />
            <FormSelectInput label="Education level" name="educationLevel" options={enumToArrayOfOptions(Education)} />
            <FormSelectInput label="Sex" name="sex" options={enumToArrayOfOptions(Sex)} />
            <FormImageInput label="Profile Image" name="profileImageData" />
            <button type="submit" className={styles.create_button}>{mode === "edit" ? "Edit" : "Create"}</button>
          </Form>
        </Formik>
      </div>
    </div>
  );
}

export default ProfileCreationPage;
