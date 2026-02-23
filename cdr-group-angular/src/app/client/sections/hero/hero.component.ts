import { Component, Input, inject } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-hero',
  standalone: false,
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss',
})
export class HeroComponent {
  translationService = inject(TranslationService);
  @Input() isSmall = false;
  @Input() title = '';

  showMore = false;

  toggleMore(): void {
    this.showMore = !this.showMore;
  }
}
