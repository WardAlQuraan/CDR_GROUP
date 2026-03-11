import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AuditLogDto } from '../../../../models/audit-log.model';
import { TranslationService } from '../../../../services/translation.service';

export interface ChangedProperty {
  key: string;
  oldValue: string;
  newValue: string;
  changed: boolean;
}

@Component({
  selector: 'app-audit-log-view-dialog',
  standalone: false,
  templateUrl: './audit-log-view-dialog.component.html',
  styleUrl: './audit-log-view-dialog.component.scss'
})
export class AuditLogViewDialogComponent {
  changes: ChangedProperty[] = [];

  constructor(
    private dialogRef: MatDialogRef<AuditLogViewDialogComponent>,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public log: AuditLogDto
  ) {
    this.changes = this.buildChanges();
  }

  get lang(): 'en' | 'ar' {
    return this.translationService.language() === 'ar' ? 'ar' : 'en';
  }


  private buildChanges(): ChangedProperty[] {
    const oldObj = this.parseJson(this.log.oldValues);
    const newObj = this.parseJson(this.log.newValues);
    const oldDisplay = this.parseJson(this.log.oldDisplayValues);
    const newDisplay = this.parseJson(this.log.newDisplayValues);

    // Build a map from raw key (e.g. CompanyId) to display key (e.g. Company)
    const displayKeyMap = new Map<string, string>();
    for (const displayKey of [...Object.keys(oldDisplay), ...Object.keys(newDisplay)]) {
      // Find the matching raw key: exact match or with "Id" suffix
      const rawKey = Object.keys(oldObj).concat(Object.keys(newObj))
        .find(k => k === displayKey || k === displayKey + 'Id');
      if (rawKey) {
        displayKeyMap.set(rawKey, displayKey);
      }
    }

    const allKeys = new Set<string>([
      ...Object.keys(oldObj),
      ...Object.keys(newObj)
    ]);

    return Array.from(allKeys).map(key => {
      const displayKey = displayKeyMap.get(key);
      const oldVal = this.getDisplayValue(oldDisplay, displayKey) ?? this.formatValue(oldObj[key]);
      const newVal = this.getDisplayValue(newDisplay, displayKey) ?? this.formatValue(newObj[key]);
      return {
        key: displayKey || key,
        oldValue: oldVal,
        newValue: newVal,
        changed: oldVal !== newVal
      };
    });
  }

  private getDisplayValue(displayObj: Record<string, any>, key?: string): string | null {
    if (!key) return null;
    const display = displayObj[key];
    if (display && typeof display === 'object' && display[this.lang]) {
      return display[this.lang];
    }
    return null;
  }

  private parseJson(value?: string): Record<string, any> {
    if (!value) return {};
    try {
      return JSON.parse(value);
    } catch {
      return {};
    }
  }

  private formatValue(value: any): string {
    if (value === null || value === undefined) return '-';
    if (typeof value === 'object') return JSON.stringify(value);
    return String(value);
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
