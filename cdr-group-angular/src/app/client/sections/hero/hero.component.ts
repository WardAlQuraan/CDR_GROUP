import { Component, Input, inject } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

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
  @Input() company?: CompanyDto;

  showMore = false;

get companyName(): string {
    if (this.company) {
      return this.translationService.language() === 'ar'
        ? this.company.nameAr
        : this.company.nameEn;
    }
    return this.translationService.translate('hero.title');
  }

  get heroTitle(): string {
    if (this.company) {
      return this.translationService.language() === 'ar'
        ? this.company.titleAr || "" 
        : this.company.titleEn  || "";
    }
    return this.translationService.translate('hero.title');
  }

  get heroSubtitle(): string {
    if (this.company) {
      return this.translationService.language() === 'ar'
        ? (this.company.descriptionAr || '')
        : (this.company.descriptionEn || '');
    }
    return this.translationService.translate('hero.subtitle');
  }

  toggleMore(): void {
    this.showMore = !this.showMore;
  }
}
