import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import styles from './GoBackButton.module.scss';

type Props = {};

function GoBackButton({}: Props) {
  const goBack = () => {
    window.history.back(); // Use browser history to go back
  };
  
  return (
    <button className={styles.go_back_button} onClick={goBack}>
      <ArrowBackIcon />
    </button>
  );
}

export default GoBackButton;
