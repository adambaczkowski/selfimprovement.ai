import styles from './AchievementsComponent.module.scss';
import LinearProgress, { linearProgressClasses } from '@mui/material/LinearProgress';
import { styled } from '@mui/material/styles';

const BorderLinearProgress = styled(LinearProgress)(({ theme }) => ({
  height: 8,
  borderRadius: 5,
  [`&.${linearProgressClasses.colorPrimary}`]: {
    backgroundColor: theme.palette.grey[theme.palette.mode === 'light' ? 300 : 800],
  },
  [`& .${linearProgressClasses.bar}`]: {
    borderRadius: 5,
    backgroundColor: theme.palette.mode === 'light' ? '#e54ee8' : '#308fe8',
  },
}));

const progressBarCount = {
  zero: 0,
  bronze: 28,
  silver: 50,
  gold: 100,
};


const achievementsNumbers = {
  bronze_achievement: 10,
  silver_achievement: 20,
  gold_achievement: 30,
};

const calculateProgress = (points: number): number => {
  if (points >= achievementsNumbers.gold_achievement) {
    return progressBarCount.gold;
  } else if (points >= achievementsNumbers.silver_achievement) {
    return progressBarCount.silver;
  } else if (points >= achievementsNumbers.bronze_achievement) {
    return progressBarCount.bronze;
  } else {
    return progressBarCount.zero;
  }
};

const getAchievementClass = (completed: number, achievement_number: number, scss_class_name: string) => {
  const className = completed < achievement_number ? `${scss_class_name}_locked` : scss_class_name;
  return styles[className];
};

function AchievementsComponent() {
  const completed = 10;

  return (
    <div className={styles.achievements}>
      <div className={styles.emoji}>🏆</div>
      <div className={styles.header_container}>
        <h1 className={styles.achievements_header}>
          Achievements
        </h1>
      </div>
      <div className={styles.achievements_container}>
        <div className={styles.achievement_items_container}>
          <div className={styles.achievement_item}>
            <div className={getAchievementClass(
              completed, 
              achievementsNumbers.bronze_achievement,
              "achievement_circle_brown"
            )}>
              🐹
            </div>
            <h1 className={styles.achievement_header}>Bronze</h1>
            <p className={styles.achievements_score}>
              {completed}/{achievementsNumbers.bronze_achievement}
            </p>
          </div>
          <div className={styles.achievement_item}>
            <div className={getAchievementClass(
              completed, 
              achievementsNumbers.silver_achievement,
              "achievement_circle_silver"
            )}>
              🐺
            </div>
            <h1 className={styles.achievement_header}>Silver</h1>
            <p className={styles.achievements_score}>
              {completed}/{achievementsNumbers.silver_achievement}
            </p>
          </div>
          <div className={styles.achievement_item}>
            <div className={getAchievementClass(
              completed, 
              achievementsNumbers.gold_achievement,
              "achievement_circle_gold"
            )}>
              🦁
            </div>
            <h1 className={styles.achievement_header}>Gold</h1>
            <p className={styles.achievements_score}>
              {completed}/{achievementsNumbers.gold_achievement}
            </p>
          </div>
        </div>
        <div className={styles.progress_bar}>
          <BorderLinearProgress variant="determinate" value={calculateProgress(completed)} />
        </div>
      </div>
    </div>
  );
}

export default AchievementsComponent;
