import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css'],
})
export class ToastComponent implements OnChanges {
  @Input() messages!: string;
  isShow!: boolean;
  isVisible!: boolean;
  HIDE_TIME_OUT = 5000;
  constructor() {}

  ngOnChanges(changes: SimpleChanges): void {
    this.messages = changes['messages'].currentValue;
    this.isShow = this.messages.length > 0;
    this.isVisible = this.messages.length > 0;

    setTimeout(() => {
      this.onCloseClicked();
    }, this.HIDE_TIME_OUT);
  }

  onCloseClicked(): void {
    this.isShow = false;

    setTimeout(() => {
      this.isVisible = false;
    }, 500);
  }
}
