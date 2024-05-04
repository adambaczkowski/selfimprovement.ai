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