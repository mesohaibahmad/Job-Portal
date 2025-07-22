export interface CreateUpdateJobPositionDto {
  title: string;
  description: string;
}

export interface JobPositionDto {
  id: string;
  title: string;
  description: string;
  isActive: boolean;
}