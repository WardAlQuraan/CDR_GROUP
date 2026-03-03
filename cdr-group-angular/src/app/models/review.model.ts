export interface ReviewDto {
  id: string;
  numberOfStars: number;
  comment: string;
  companyId?: string;
  companyNameEn?: string;
  companyNameAr?: string;
  isVisible: boolean;
  createdAt: Date;
}

export interface CreateReviewDto {
  numberOfStars: number;
  comment: string;
  companyId?: string;
}

export interface UpdateReviewDto {
  numberOfStars?: number;
  comment?: string;
  isVisible?: boolean;
  companyId?: string;
}

export interface ReviewPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  isVisible?: boolean;
  companyId?: string;
}
