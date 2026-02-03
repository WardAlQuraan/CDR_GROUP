import { Component, inject } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  translationService = inject(TranslationService);
  currentYear = new Date().getFullYear();
}
