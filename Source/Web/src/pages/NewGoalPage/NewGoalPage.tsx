import { useNavigate } from 'react-router-dom';
import { Form, Formik } from "formik";
import { FormSelectInput, LoadingCircle } from "../../components/componentsIndex";
import { NewGoalFormValidation } from "./NewGoalFormValidationSchema";
import styles from "./NewGoalPage.module.scss";
import { CreateGoalCommand, Experience, GoalCategories, LearningType, TimeAvailability } from "../../utils/api/goal";
import { enumToArrayOfOptions } from "../../utils/helpers/enumToArrayOfOptions";
import FormNumberInput from "../../components/Formik/FormNumberInput/FormNumberInput";
import { useMutation } from "react-query";
import { createGoal } from "../../utils/services/goalService";
import { useState } from 'react';

interface Props {}

const NewGoalPage = ({}: Props) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  
  const goalInitialValues: CreateGoalCommand = {
    category: undefined,
    timeAvailability: undefined,
    duration: undefined,
    experience: undefined,
    learningType: undefined,
  };

  const createGoalMutation = useMutation({
    mutationFn: (command: CreateGoalCommand) => createGoal(command),
    onSuccess: (response) => {
      console.log("success");
    },
    onError: () => {
      console.log("fail");
    },
  });

  const handleCreateGoal = async (values: CreateGoalCommand) => {
    setIsLoading(true);
    try {
      const response = await createGoal(values); 
      const goalId = response?.data?.id;
      navigate(`/goal/${goalId}`);
    } catch (error) {
      console.error('Error creating goal:', error);
      // Handle error if needed
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className={styles.background_container}>
      {isLoading ? (
        <LoadingCircle timeout={10000000} heading='Creating new goal...' errorMessage="Something went wrong" />
      ) : (
        <div className={styles.extended_background_container}>
          <Formik
            initialValues={goalInitialValues}
            onSubmit={(values) => {
              handleCreateGoal(values);
            }}
            validationSchema={null} //NewGoalFormValidation}
            validateOnChange={false}
            validateOnBlur={false}
          >
            <Form className={styles.form_items_container}>
              <h1 className={styles.heading}>New Goal</h1>
              <FormSelectInput label="Category" name="category" options={enumToArrayOfOptions(GoalCategories)} />
              <FormSelectInput label="Time Availability" name="timeAvailability" options={enumToArrayOfOptions(TimeAvailability)} />
              <FormNumberInput label="Duration" name="duration" />
              <FormSelectInput label="Experience" name="experience" options={enumToArrayOfOptions(Experience)} />
              <FormSelectInput label="Learning Type" name="learningType" options={enumToArrayOfOptions(LearningType)} />
              <button className={styles.create_button}>Create</button>
            </Form>
          </Formik>
        </div>
      )}
    </div>
  );
};

export default NewGoalPage;
