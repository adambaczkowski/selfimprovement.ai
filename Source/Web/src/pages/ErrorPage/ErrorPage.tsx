import styles from './ItemsGrid.module.scss'; // Import the SCSS file

const ErrorPage = () => {
  return (
    <div className={styles.background_container}>
      <h1 className={styles.header}>404 Not Found</h1>
      <p>Sorry, the page you're looking for doesn't exist.</p>
    </div>
  );
};

export default ErrorPage;