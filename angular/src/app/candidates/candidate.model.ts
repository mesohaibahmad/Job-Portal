export interface CandidateDto {
  id: string;
  fullName: string;
  email: string;
  resumePath: string;
  jobPositionId: string;
  jobPositionTitle: string;
}

export interface CreateUpdateCandidateDto {
  fullName: string;
  email: string;
  phone: string;
  jobPositionId: string;
  resume?: File;
}
