import React, { useEffect, useState } from 'react';
import { Drawer, Typography, Box, ListItemButton } from '@mui/material';
import { ModalClose, Menu, IconButton, List } from '@mui/joy';
import MenuIcon from '@mui/icons-material/Menu';
import { LoadingCircle, DailyTaskList } from "../../components/componentsIndex"
import { Link } from 'react-router-dom';
import styles from './TaskItem.module.scss'; // Import the SCSS file

interface Props {
  title: string;
  description: string;
  date: string;
  isCompleted: boolean;
}

function TaskItem({ title, description, date, isCompleted }: Props) {
  return (
    <Link to={`/task`} className={styles.task_item}>
      <h1>{title}</h1>
      <p className={styles.description}>{description}</p>
      <p className={styles.date}>{date}</p>
      <div className={styles.task_footer}>
        {isCompleted ? (
          <button
            className={styles.complete_button}
            onClick={() => {
              const task = {
                isCompleted: !isCompleted,
              };

              // updateTask(task);
            }}
          >
            Completed
          </button>
        ) : (
          <button
            className={styles.incomplete_button}
            onClick={() => {
              const task = {
                isCompleted: !isCompleted,
              };

              // updateTask(task);
            }}
          >
            Incomplete
          </button>
        )}
      </div>
    </Link>
  );
}

export default TaskItem;
