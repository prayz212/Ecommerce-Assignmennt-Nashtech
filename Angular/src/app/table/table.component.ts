import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'body-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnChanges {
  @Input() columns: string[] = [];
  @Input() data: any[] = [];
  @Output() rowClickEvent = new EventEmitter();

  rows: any[] = [];

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.rows = this.formatData(changes['data'].currentValue);
  }

  formatData(data: any[]): any[] {
    return data.map((item: any) => {
      const values = Object.values(item);
      return values.map((value: any) => {
        return typeof value === 'boolean'
          ? (value ? "Có" : "Không")
          : value
      });
    });
  }

  onRowClick(id: any): void {
    this.rowClickEvent.emit(id);
  }
}
