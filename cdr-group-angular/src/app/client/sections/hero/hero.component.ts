import { Component, OnInit, OnDestroy, Input, inject } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-hero',
  standalone: false,
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss',
})
export class HeroComponent implements OnInit, OnDestroy {
  translationService = inject(TranslationService);
  @Input() isSmall = false;
  @Input() title = '';

  words = ['hero.modern', 'hero.creative', 'hero.lifestyle'];
  currentWordIndex = 0;
  isVisible = true;
  private intervalId: ReturnType<typeof setInterval> | null = null;

  ngOnInit() {
    if (!this.isSmall) {
      this.intervalId = setInterval(() => {
        this.isVisible = false;
        setTimeout(() => {
          this.currentWordIndex = (this.currentWordIndex + 1) % this.words.length;
          this.isVisible = true;
        }, 600);
      }, 2500);
    }
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  get currentWord(): string {
    return this.words[this.currentWordIndex];
  }
}
