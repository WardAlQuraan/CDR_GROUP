export interface FileAttachmentDto {
  id: string;
  fileName: string;
  storedFileName: string;
  fileUrl: string;
  path: string;
  contentType: string;
  size: number;
  entityId?: string;
  entityType?: string;
  description?: string;
  isPrimary: boolean;
  createdAt: Date;
  updatedAt?: Date;
  createdBy?: string;
}

export interface CreateFileAttachmentDto {
  file: File;
  entityId?: string;
  entityType?: string;
  description?: string;
  removeOldFiles?: boolean;
  isPrimary?: boolean;
}

export interface UpdateFileAttachmentDto {
  entityId?: string;
  entityType?: string;
  description?: string;
  isPrimary?: boolean;
}

export interface BulkFileOperationItemDto {
  file?: File;
  entityId: string;
  entityType: string;
  fileId?: string;
  description?: string;
  isPrimary?: boolean;
}

export interface BulkFileOperationResultDto {
  entityId: string;
  entityType: string;
  fileId?: string;
  operation: string;
  filePath?: string;
  errorMessage?: string;
}
