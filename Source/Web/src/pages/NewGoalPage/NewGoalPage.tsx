import { Form, Formik } from "formik";
import { FormSelectInput } from "../../components/componentsIndex";
import { NewGoalFormValidation } from "./NewGoalFormValidationSchema";
import styles from "./NewGoalPage.module.scss";
import { CreateGoalCommand, Experience, GoalCategories, LearningType, TimeAvailability } from "../../utils/api/goal";
import { enumToArrayOfOptions } from "../../utils/helpers/enumToArrayOfOptions";
import FormNumberInput from "../../components/Formik/FormNumberInput/FormNumberInput";
import { useMutation } from "react-query";
import { createGoal } from "../../utils/services/goalService";

interface Props {}

const NewGoalPage = ({}: Props) => {
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
    console.log("#values", values);
    await createGoalMutation.mutateAsync(values);
  };

  return (
    <div className={styles.background_container}>
      <div className={styles.extended_background_container}>
        <Formik
          initialValues={goalInitialValues}
          onSubmit={(values) => {
            handleCreateGoal(values);
          }}
          validationSchema={NewGoalFormValidation}
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
    </div>
  );
};

export default NewGoalPage;
