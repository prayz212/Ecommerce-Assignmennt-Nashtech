import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'body-top-section',
  templateUrl: './top-section.component.html',
  styleUrls: ['./top-section.component.css'],
})
export class TopSectionComponent implements OnInit {
  @Input() titleText: string | undefined;
  @Input() buttonText: string | undefined;
  @Input() redirectTo: string | undefined;
  @Input() enableButtonCreate: boolean = false;
  @Input() enableButtonBack: boolean = false;

  constructor(private router: Router) {}

  ngOnInit(): void {}

  goToCreate(): void {
    if (this.enableButtonCreate) this.router.navigate([this.redirectTo]);
  }

  goBack(): void {
    if (this.enableButtonBack) this.router.navigate([this.redirectTo]);
  }
}
