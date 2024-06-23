import { useState } from "react";
import { useQuery } from "react-query";
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { menu, signOutIcon } from "./../../utils/enums/sidebarMenu";
import styles from './Sidebar.module.scss';
import { UserProfileDto } from "../../utils/api/identity";
import { jwtDecode } from 'jwt-decode';
import { fetchUser } from "../../utils/services/userService";

interface JwtPayload {
  unique_name: string;
}

function Sidebar() {
  const [user, setUser] = useState<UserProfileDto | null>(null);
  const [profileImage, setProfileImage] = useState<string>('');
  const pathname = useLocation().pathname;
  const navigate = useNavigate();

  useQuery({
    queryKey: ["getUser"],
    queryFn: async () => {
      const user = await fetchUser();
      if (user) {
        setUser(user);
        if (user?.profileImageData) {
          handleBase64ToImage(user.profileImageData, 'yourImageElementId');
        } else {
          setProfileImage('https://i0.wp.com/www.repol.copl.ulaval.ca/wp-content/uploads/2019/01/default-user-icon.jpg?ssl=1');
        }
      }
      return user;
    },
    refetchOnWindowFocus: false,
  });

  const handleBase64ToImage = (base64: string, imgElementId: string) => {
    // Convert base64 string to a Blob
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length).fill(0).map((_, i) => byteCharacters.charCodeAt(i));
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'image/jpeg' }); // Adjust the MIME type if necessary
  
    // Create a URL for the Blob and set it as the image source
    const imageUrl = URL.createObjectURL(blob);
    setProfileImage(imageUrl);
  };

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
          <img id="yourImageElementId" src={profileImage} alt="Your Image" className={styles.image} />
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
