import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-back-button',
  standalone: false,
  template: `
    <button mat-icon-button (click)="goBack()" class="back-button mb-2"
      [style.float]="isArabic ? 'right' : 'left'"
      style="color: var(--primary-color); background-color: var(--secondary-color); display: flex; align-items: center; justify-content: center;">
      <mat-icon>{{ isArabic ? 'arrow_forward' : 'arrow_back' }}</mat-icon>
    </button>
  `
})
export class BackButtonComponent {
  constructor(private location: Location, private translationService: TranslationService) {}

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  goBack(): void {
    this.location.back();
  }
}
