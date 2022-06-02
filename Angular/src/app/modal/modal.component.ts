import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit, OnChanges {
  @Input() messages: string | undefined;
  @Input() confirmTextButton: string | undefined;
  @Input() cancelTextButton: string | undefined;
  @Input() show: boolean | undefined;

  @Output() confirmClick = new EventEmitter();
  @Output() cancelClick = new EventEmitter();

  isShow = true;
  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.isShow = changes['show'].currentValue;
  }

  onConfirmClick(): void {
    this.confirmClick.emit();
  }

  onCancelClick(): void {
    this.cancelClick.emit();
  }
}
