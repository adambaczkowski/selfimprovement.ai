import { UserService, UserProfileDto } from "../api/identity"
import { base64ToBlob } from "../helpers/stringToBlob"

export const createUser = async (command : UserProfileDto) => {
  const response = await UserService.postProfile({
    formData: {
      UserId: command.id, 
      Weight: command.weight !== null ? command.weight : undefined,
      Height: command.height !== null ? command.height : undefined,
      Age: command.age !== null ? command.age : undefined,
      EducationLevel: command.educationLevel !== null ? command.educationLevel : undefined, 
      Sex: command.sex !== null ? command.sex : undefined, 
      ProfileImage: command.profileImageData ? base64ToBlob(command.profileImageData, "image/jpeg") : undefined, 
    }});
  return response;
}

export const editUser = async (command : UserProfileDto) => {
  const response = await UserService.putProfile({
    formData: {
      UserId: command.id, 
      Weight: command.weight !== null ? command.weight : undefined,
      Height: command.height !== null ? command.height : undefined,
      Age: command.age !== null ? command.age : undefined,
      EducationLevel: command.educationLevel !== null ? command.educationLevel : undefined, 
      Sex: command.sex !== null ? command.sex : undefined, 
      ProfileImage: command.profileImageData ? base64ToBlob(command.profileImageData, "image/jpeg") : undefined, 
    }});
  return response;
}

export const fetchUser = async () => {
  return await UserService.getProfile();
}