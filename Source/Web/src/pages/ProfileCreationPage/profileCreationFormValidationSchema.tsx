import * as Yup from "yup";

export const profileCreationFormValidation = () => {
  const passwordRegExp = /^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$/;

  return Yup.object().shape({
    email: Yup.string().email("test").required("test"),
    password: Yup.string()
      //.matches(passwordRegExp,'Password has to be at least 8 characters long,contain 1 special sign and 1 uppercase letter')
      .required("Password is required"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password")], "Passwords must match")
      .required("You must confirm your password"),
  });
};
