import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CREATE_FORM_TYPE, EDIT_FORM_TYPE } from 'src/constants/variables';
import { Category } from '../category.model';

@Component({
  selector: 'category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css'],
})
export class CategoryFormComponent implements OnInit, OnChanges {
  @Input() type: string = CREATE_FORM_TYPE;
  @Input() formData: Category | undefined;

  category!: Category;
  isEdit!: boolean;

  @Output() onSubmitEvent = new EventEmitter();

  constructor() {}

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.isEdit = this.type === EDIT_FORM_TYPE;

    if (this.isEdit)
      this.category = changes['formData'].currentValue;
    else
      this.category = new Category();

  }

  onSubmit(formData: NgForm): void {
    const emitData: any = {
      valid: formData.valid,
      data: formData.value,
    };

    this.onSubmitEvent.emit(emitData);
  }
}
