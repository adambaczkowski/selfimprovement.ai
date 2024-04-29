import React from 'react';
import { Link, useLocation  } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import menu from "./../../utils/enums/sidebarMenu";
import styles from './Sidebar.module.scss'; // Import the SCSS file
import { IconProp } from '@fortawesome/fontawesome-svg-core';

function Sidebar() {
  const [open, setOpen] = React.useState(false);
  const pathname = useLocation().pathname;

  return (
    <div className={styles.sidebar_background_container}>
      <div className={styles.image_container}>
        <img src="https://assets.vogue.com/photos/6327939f06377e01c5304296/master/w_1920,c_limit/Fc9-RcUXgAEgljY.jpeg" alt="Your Image" className={styles.image} />
        <h1 className={styles.sidebar_header}>Adam Kowalski</h1>
      </div>
      <div className={styles.list_container}>
        {menu.map((item) => {
          const link = item.link;
          return (
            <Link to={link} className={`${pathname === link ? styles.nav_item_activate : styles.nav_item}`} key={item.id}>
              <FontAwesomeIcon icon={item.icon as IconProp} />
              <p className={styles.link_title}>{item.title}</p>
            </Link>
          )
        })}
      </div>
    </div>
  );
}

export default Sidebar;

{/* 

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
          <ListItemButton>Goals</ListItemButton>
          <ListItemButton>Calendar</ListItemButton>
          <ListItemButton>Completed</ListItemButton>
          <ListItemButton>New Goal!</ListItemButton>
        </List>
      </div>
    </Drawer>
</div> 

<Link to="/tasks" className={styles.nav_item}>All tasks</Link>
<Link to="/goals" className={styles.nav_item}>Goals</Link>
<Link to="/calendar" className={styles.nav_item}>Calendar</Link>
<Link to="/" className={styles.nav_item}>Completed</Link>
<Link to="/" className={styles.nav_item}>New Goal</Link>

*/}
