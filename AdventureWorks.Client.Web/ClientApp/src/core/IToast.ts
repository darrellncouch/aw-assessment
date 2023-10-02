export interface IToast {
  show: boolean;
  message: string;
  style: ToastType;
}

export enum ToastType {
  info = "infoToast",
  warn = 'warnToast',
  error = 'errorToast',
  none = "hiddenToast"
}
