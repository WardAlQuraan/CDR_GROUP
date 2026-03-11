export interface AuditLogDto {
  id: string;
  entityName: string;
  entityId: string;
  actionType: string;
  oldValues?: string;
  newValues?: string;
  oldDisplayValues?: string;
  newDisplayValues?: string;
  affectedColumns?: string;
  performedBy?: string;
  timestamp: Date;
}
