import React from 'react';
import { Drawer, Typography, Box, ListItemButton } from '@mui/material';
import { ModalClose, Menu, IconButton, List } from '@mui/joy';
import MenuIcon from '@mui/icons-material/Menu';
import styles from './Sidebar.module.scss'; // Import the SCSS file

function Sidebar() {
  const [open, setOpen] = React.useState(false);

  return (
    <div className='sidebar'>
      <IconButton 
        color='primary'
        onClick={() => setOpen(true)}
        className={styles.menu_button} // Apply the menu-button class
      >
        <MenuIcon color="disabled" />
      </IconButton>
      <Drawer
        open={open}
        onClick={() => setOpen(false)}
        PaperProps={{
            className: styles.drawer, // Apply the drawer class
        }}
        >
        <Box className={styles.close_button}>
          <ModalClose id="close-icon" sx={{ position: 'initial' }} />
        </Box>
        <div className={styles.imageContainer}>
          <img src="https://assets.vogue.com/photos/6327939f06377e01c5304296/master/w_1920,c_limit/Fc9-RcUXgAEgljY.jpeg" alt="Your Image" className={styles.image} />
        </div>
          <div className={styles.listContainer}>
            <List size="lg" component="nav" className={styles.list}>
              <ListItemButton sx={{ fontWeight: 'lg' }}>Home</ListItemButton>
              <ListItemButton>All tasks</ListItemButton>
              <ListItemButton>Calendar</ListItemButton>
              <ListItemButton>Completed</ListItemButton>
              <ListItemButton>New Goal!</ListItemButton>
            </List>
          </div>
        </Drawer>
    </div>
  );
}

export default Sidebar;
