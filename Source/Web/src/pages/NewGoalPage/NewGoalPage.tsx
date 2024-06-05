import { useNavigate } from 'react-router-dom';
import { Form, Formik } from "formik";
import { FormSelectInput, LoadingCircle } from "../../components/componentsIndex";
import { NewGoalFormValidation } from "./NewGoalFormValidationSchema";
import styles from "./NewGoalPage.module.scss";
import { CreateGoalCommand, Experience, GoalCategories, Goals, LearningType, TimeAvailabilityPerDay, TimeAvailabilityPerWeek } from "../../utils/api/goal";
import { enumToArrayOfOptions } from "../../utils/helpers/enumToArrayOfOptions";
import FormNumberInput from "../../components/Formik/FormNumberInput/FormNumberInput";
import FormTextInput from "../../components/Formik/FormTextInput/FormTextInput";
import { useMutation } from "react-query";
import { createGoal } from "../../utils/services/goalService";
import { useState } from 'react';

interface Props {}

const NewGoalPage = ({}: Props) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  
  const goalInitialValues: CreateGoalCommand = {
    name: undefined,
    category: undefined,
    goalFriendlyName: undefined,
    timeAvailabilityPerDay: undefined,
    timeAvailabilityPerWeek: undefined,
    duration: undefined,
    userAdvancement: undefined,
    learningType: undefined,
    userInput: undefined
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
      const goal = await createGoal(values); 
      const goalId = goal?.id;
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
              <FormTextInput label="Goal name" name="name" placeholderText="Marathon"/>
              <FormSelectInput label="Category" name="category" options={enumToArrayOfOptions(GoalCategories)} />
              <FormSelectInput label="Specific category" name="goalFriendlyName" options={enumToArrayOfOptions(Goals)} />
              <FormSelectInput label="Time availability per day" name="timeAvailabilityPerDay" options={enumToArrayOfOptions(TimeAvailabilityPerDay)} />
              <FormSelectInput label="Time availability per week" name="timeAvailabilityPerWeek" options={enumToArrayOfOptions(TimeAvailabilityPerWeek)} />
              <FormNumberInput label="Duration" name="duration" />
              <FormSelectInput label="Experience" name="experience" options={enumToArrayOfOptions(Experience)} />
              <FormSelectInput label="Learning type" name="learningType" options={enumToArrayOfOptions(LearningType)} />
              <FormTextInput label="Your more specific description ;)" name="userInput" placeholderText="Marathon"/>
              <button className={styles.create_button}>Create</button>
            </Form>
          </Formik>
        </div>
      )}
    </div>
  );
};

export default NewGoalPage;
