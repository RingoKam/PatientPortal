import { values } from 'lodash'

export const resolveErrorMsg = (err: any, defaultMsg: string): string => {
  try {
    if (err && err.response) {
      const { errors } = JSON.parse(err.response);
      return values(errors).reduce((acc: string[], cur: any) => {
        acc.push(values(cur).join(", "));
        return acc;
      }, []).join(", ");
    } else {
      return defaultMsg;
    }
  } catch (e) {
    console.error(e);
    return defaultMsg;
  }
}
