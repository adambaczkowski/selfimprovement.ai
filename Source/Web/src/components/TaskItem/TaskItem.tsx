import React, { useEffect, useState } from 'react';
import { Drawer, Typography, Box, ListItemButton } from '@mui/material';
import { ModalClose, Menu, IconButton, List } from '@mui/joy';
import MenuIcon from '@mui/icons-material/Menu';
import { LoadingCircle, DailyTaskList } from "../../components/componentsIndex"
import styles from './TaskItem.module.scss'; // Import the SCSS file

interface Props {
  title: string;
  description: string;
  date: string;
  isCompleted: boolean;
}

function TaskItem({ title, description, date, isCompleted }: Props) {
  return (
    <div className={styles.TaskItemStyled}>
      <h1>{title}</h1>
      <p>{description}</p>
      {/* <p className="date">{formatDate(date)}</p>1 */}
      <p className={styles.date}>{date}</p>
      <div className={styles.task_footer}>
        {isCompleted ? (
          <button
            className={styles.completed}
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
            className={styles.incomplete}
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
        {/* <button className="edit">{edit}</button> */}
        {/* <button
          className={styles.delete}
          onClick={() => {
            // deleteTask(id);
          }}
        > */}
          {/* {trash} */}
        {/* </button> */}
      </div>
    </div>
  );
}

export default TaskItem;
