import { addSpacesBeforeCapitals } from "./addSpacesBeforeCapitals";

export const enumToArrayOfOptions = (enumeration: any) => {
  return Object.values(enumeration).map((value: any) => ({
    label: addSpacesBeforeCapitals(value),
    value: value,
  }));
};