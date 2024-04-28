import React from 'react';
import { ListOwnProps, Paper } from '@mui/material';
import styles from './NumberListBox.module.scss';

interface ListProps {
  items: string[];
}

function numberToString(number: number) {
  const numberMap: { [key: number]: string } = {
    0: 'zero',
    1: 'one',
    2: 'two',
    3: 'three',
    4: 'four',
    5: 'five',
    6: 'six',
    7: 'seven',
    8: 'eight',
    9: 'nine'
  };

  return numberMap[number];
}

function DailyTaskList({ items }: ListProps) {
  return (
    <div className={styles.center_container}>
      <Paper elevation={3} className={styles.glass_container}>
        <ol className={styles.list}>
          {items.map((task, taskIndex) => (
            <li key={taskIndex} className={styles.item}>
              <h2 className={styles.headline}>task {numberToString(taskIndex + 1)}</h2>
              <span>
                {task}
              </span>
            </li>
          ))}
        </ol>
      </Paper>
    </div>
  );
};

export default DailyTaskList;
