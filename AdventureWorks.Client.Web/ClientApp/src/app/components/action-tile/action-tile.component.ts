import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-action-tile',
  templateUrl: './action-tile.component.html',
  styleUrls: ['./action-tile.component.scss']
})
export class ActionTileComponent {

  constructor(private readonly router: Router) {

  }

  @Input()
  public icon: string = "";

  @Input()
  public routerLink: string = "";

  @Input()
  public text: string = "";

  public onClick(): void {
    this.router.navigate([this.routerLink])
  }
}
