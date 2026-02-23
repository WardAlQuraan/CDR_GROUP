import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AuditLogDto } from '../../../../models/audit-log.model';

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
    @Inject(MAT_DIALOG_DATA) public log: AuditLogDto
  ) {
    this.changes = this.buildChanges();
  }


  private buildChanges(): ChangedProperty[] {
    const oldObj = this.parseJson(this.log.oldValues);
    const newObj = this.parseJson(this.log.newValues);

    const allKeys = new Set<string>([
      ...Object.keys(oldObj),
      ...Object.keys(newObj)
    ]);

    return Array.from(allKeys).map(key => {
      const oldVal = this.formatValue(oldObj[key]);
      const newVal = this.formatValue(newObj[key]);
      return {
        key,
        oldValue: oldVal,
        newValue: newVal,
        changed: oldVal !== newVal
      };
    });
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
