import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-back-button',
  standalone: false,
  template: `
    <button mat-icon-button (click)="goBack()" class="back-button mb-2"
      style="color: var(--primary-color); background-color: var(--secondary-color); display: flex; align-items: center; justify-content: center;">
      <mat-icon>arrow_back</mat-icon>
    </button>
  `
})
export class BackButtonComponent {
  constructor(private location: Location) {}

  goBack(): void {
    this.location.back();
  }
}
