export const enumToArrayOfOptions = (enumeration: any) => {
  return Object.values(enumeration).map((value: any) => ({
    label: addSpacesBeforeCapitals(value),
    value: value,
  }));
};

function addSpacesBeforeCapitals(enumeration: string) {
  const str = enumeration.toString();
  return str.replace(/([a-z])([A-Z])/g, '$1 $2');
}