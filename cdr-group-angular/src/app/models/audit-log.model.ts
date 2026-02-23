export interface AuditLogDto {
  id: string;
  entityName: string;
  entityId: string;
  actionType: string;
  oldValues?: string;
  newValues?: string;
  affectedColumns?: string;
  performedBy?: string;
  timestamp: Date;
}
