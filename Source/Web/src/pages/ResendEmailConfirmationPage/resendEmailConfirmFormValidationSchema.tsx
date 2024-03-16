import * as Yup from "yup";

export const resendEmailConfirmFormValidation = () => {
  return Yup.object().shape({
    email: Yup.string().email().required(),
  });
};
