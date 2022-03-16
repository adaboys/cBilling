import vi from './vi';
import en from './en';
const messages = {
  en,
  vi,
};

export default locale => {
  let result = messages[locale];
  return result;
};
