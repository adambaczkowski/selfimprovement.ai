import { ButtonProps } from "@mui/material";
import styles from './CustomButton.module.scss';

interface Props extends ButtonProps {
  text: string;
}

const CustomButton = (props: Props) => {
  return <button {...props} className={styles.sign_in_button}>{props.text}</button>;
};

export default CustomButton;
