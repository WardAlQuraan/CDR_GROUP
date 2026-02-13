import { TranslationService } from '../services/translation.service';

export function buildSearchPlaceholder(translationService: TranslationService, properties: string[], prefix: string): string {
  const fields = properties.map(p => translationService.translate(`${prefix}.${p}`)).join('\n');
  return `${translationService.translate('common.searchBy')} ${fields}`;
}
