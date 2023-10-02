import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { IToast, ToastType } from 'src/core/IToast';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private inactivetoast: IToast = {
    show: false,
    message: "",
    style: ToastType.none
  };

  private toast = new BehaviorSubject<IToast>(this.inactivetoast);
  public _toast = this.toast.asObservable();

  public showToast(type: ToastType, message: string): void {
    this.toast.next({
      show: true,
      message: message,
      style: type
    });

    setTimeout(() => this.dismissToast(), 5000)
  }

  public dismissToast = (): void => this.toast.next(this.inactivetoast);
}
