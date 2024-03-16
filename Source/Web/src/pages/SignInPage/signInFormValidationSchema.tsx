import * as Yup from "yup";

export const signInFormValidation = () => {
  return Yup.object().shape({
    email: Yup.string().email("test").required("test"),
    password: Yup.string().required(),
  });
};
