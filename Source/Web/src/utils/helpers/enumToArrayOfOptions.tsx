export const enumToArrayOfOptions = (enumeration: any) => {
  return Object.values(enumeration).map((value: any) => ({
    label: value,
    value: value,
  }));
};
