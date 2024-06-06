import { useState } from "react";
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { menu, signOutIcon} from "./../../utils/enums/sidebarMenu";
import styles from './Sidebar.module.scss';
import { UserProfileDto } from "../../utils/api/identity";
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  unique_name: string;
}

function Sidebar() {
  const [user, setUser] = useState<UserProfileDto | null>(null);
  const pathname = useLocation().pathname;
  const navigate = useNavigate();

  function getUsernameFromToken(token: string): string | null {
    try {
      const decodedToken = jwtDecode<JwtPayload>(token);
      return decodedToken.unique_name;
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }

  const token = localStorage.getItem('userToken');
  const username = getUsernameFromToken(token as string);

  const handleSignOut = async () => {
    try {
      localStorage.removeItem('userToken');
      return navigate("/signIn");
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className={styles.sidebar_background_container}>
      <div className={styles.image_container}>
        <Link to={"/profileCreation/edit"} className={styles.image} title='Edit profile'>
          <img src="https://assets.vogue.com/photos/6327939f06377e01c5304296/master/w_1920,c_limit/Fc9-RcUXgAEgljY.jpeg" alt="Your Image" className={styles.image} />
        </Link>
        <h1 className={styles.sidebar_header}>{username}</h1>
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
      <div className={styles.sign_out_container}>
        <button className={styles.sign_out} onClick={handleSignOut}>
          <FontAwesomeIcon icon={signOutIcon as IconProp} />
          <span>Sign Out</span>
        </button>
      </div>
    </div>
  );
}

export default Sidebar;