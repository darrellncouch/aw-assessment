import { Component } from '@angular/core';
import { UiService } from './service/ui.service';
import { IToast, ToastType } from 'src/core/IToast';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  title = 'Adventure Works';

  public toast: IToast = {show: false, message: "", style: ToastType.none};

  constructor(private readonly uiService: UiService) {}

  ngOnInit() {
    this.uiService._toast.subscribe(t => this.toast = t)
  }

  public dismissToast(): void {
    this.uiService.dismissToast();
  }
}
