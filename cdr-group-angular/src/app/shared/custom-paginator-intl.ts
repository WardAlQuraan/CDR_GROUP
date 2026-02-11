import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { TranslationService } from '../services/translation.service';

@Injectable()
export class CustomPaginatorIntl extends MatPaginatorIntl {
  constructor(private translationService: TranslationService) {
    super();
    this.updateLabels();
  }

  private updateLabels(): void {
    this.itemsPerPageLabel = this.translationService.translate('common.itemsPerPage');
    this.firstPageLabel = this.translationService.translate('common.firstPage');
    this.lastPageLabel = this.translationService.translate('common.lastPage');
    this.nextPageLabel = this.translationService.translate('common.nextPage');
    this.previousPageLabel = this.translationService.translate('common.previousPage');
  }

  override getRangeLabel = (page: number, pageSize: number, length: number): string => {
    const showing = this.translationService.translate('common.showing');
    const of = this.translationService.translate('common.of');
    const isRtl = this.translationService.isRtl();
    const lrm = isRtl ? '\u200E' : '';

    if (length === 0 || pageSize === 0) {
      return `${showing} ${lrm}0${lrm} ${of} ${lrm}${length}${lrm}`;
    }

    const startIndex = page * pageSize;
    const endIndex = Math.min(startIndex + pageSize, length);
    return `${showing} ${lrm}${startIndex + 1}-${endIndex}${lrm} ${of} ${lrm}${length}${lrm}`;
  };
}
