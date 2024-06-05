export const addSpacesBeforeCapitals = (enumeration: string) => {
    const str = enumeration.toString();
    return str.replace(/([a-z])([A-Z])/g, '$1 $2');
}