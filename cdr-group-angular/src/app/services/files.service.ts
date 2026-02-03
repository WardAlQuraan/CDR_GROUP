import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  FileAttachmentDto,
  CreateFileAttachmentDto,
  UpdateFileAttachmentDto,
  BulkFileOperationItemDto,
  BulkFileOperationResultDto
} from '../models/file-attachment.model';

@Injectable({
  providedIn: 'root'
})
export class FilesService extends BaseService<FileAttachmentDto, CreateFileAttachmentDto, UpdateFileAttachmentDto> {

  constructor(http: HttpClient) {
    super(http, 'files');
  }

  upload(dto: CreateFileAttachmentDto): Observable<ApiResponse<FileAttachmentDto>> {
    const formData = new FormData();
    formData.append('file', dto.file);

    if (dto.entityId) {
      formData.append('entityId', dto.entityId);
    }
    if (dto.entityType) {
      formData.append('entityType', dto.entityType);
    }
    if (dto.description) {
      formData.append('description', dto.description);
    }
    if (dto.removeOldFiles) {
      formData.append('removeOldFiles', 'true');
    }
    if (dto.isPrimary !== undefined) {
      formData.append('isPrimary', dto.isPrimary.toString());
    }

    return this.http.post<ApiResponse<FileAttachmentDto>>(this.getApiUrl(), formData).pipe(
      catchError(error => this.handleError(error))
    );
  }

  bulkUpload(items: BulkFileOperationItemDto[]): Observable<ApiResponse<BulkFileOperationResultDto[]>> {
    const formData = new FormData();

    items.forEach((item, index) => {
      if (item.file) {
        formData.append(`items[${index}].file`, item.file);
      }
      formData.append(`items[${index}].entityId`, item.entityId);
      formData.append(`items[${index}].entityType`, item.entityType);
      if (item.fileId) {
        formData.append(`items[${index}].fileId`, item.fileId);
      }
      if (item.description) {
        formData.append(`items[${index}].description`, item.description);
      }
      if (item.isPrimary !== undefined) {
        formData.append(`items[${index}].isPrimary`, item.isPrimary.toString());
      }
    });

    return this.http.post<ApiResponse<BulkFileOperationResultDto[]>>(`${this.getApiUrl()}/bulk`, formData).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByEntityId(entityId: string): Observable<ApiResponse<FileAttachmentDto[]>> {
    return this.get<FileAttachmentDto[]>(`/by-entity/${entityId}`);
  }

  getByEntityType(entityType: string): Observable<ApiResponse<FileAttachmentDto[]>> {
    return this.get<FileAttachmentDto[]>(`/by-type/${encodeURIComponent(entityType)}`);
  }

  getByEntity(entityId: string, entityType: string): Observable<ApiResponse<FileAttachmentDto[]>> {
    return this.get<FileAttachmentDto[]>(`/by-entity/${entityId}/${encodeURIComponent(entityType)}`);
  }
}
