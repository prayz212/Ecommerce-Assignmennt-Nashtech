import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'table-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css'],
})
export class PaginationComponent implements OnChanges {
  @Input() current: number = 0;
  @Input() total: number = 0;
  @Output() pageClickEvent = new EventEmitter();

  pages: number[] = [];

  constructor() {}

  ngOnChanges(): void {
    this.calculateDisplayPages();
  }

  calculateDisplayPages(): void {
    const numberOfPage = 5;
    const rank = this.current / numberOfPage;
    let from = -2;

    if (rank === 0.2) {
      from = 0;
    } else if (rank === 0.4) {
      from = -1;
    }

    this.pages = Array.from(
      [...Array(numberOfPage)].fill(from),
      (value: number, index: number): number => value + index + rank * numberOfPage
    ).filter((value: number): boolean => value <= this.total);
  }

  onPageClicked(pageNumber: number): void {
    this.pageClickEvent.emit(pageNumber);
  }
}
