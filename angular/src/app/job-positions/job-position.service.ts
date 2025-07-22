
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobPositionDto, CreateUpdateJobPositionDto } from './job-position.models';
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class JobPositionService {
 private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

      

  // Get All with pagination/filtering support
  getAllJobs(input?: any): Observable<any> {

       let url_ = this.baseUrl + "/api/services/app/JobPosition";
        url_ = url_.replace(/[?&]$/, "");

    const params = new HttpParams({ fromObject: input || {} });
    return this.http.get<{ items: JobPositionDto[], totalCount: number }>(
      `${url_}/GetAllJobs`,
      { params }
    );
  }

  

  // Get single item by ID
  get(id: string): Observable<JobPositionDto> {
         let url_ = this.baseUrl + "/api/services/app/JobPosition";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.get<JobPositionDto>(`${url_}/Get?id=${id}`);
  }

  // Create new job position
  create(input: CreateUpdateJobPositionDto): Observable<JobPositionDto> {
         let url_ = this.baseUrl + "/api/services/app/JobPosition";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.post<JobPositionDto>(`${url_}/Create`, input);
  }

  // Update existing job position
  update(id: string, input: CreateUpdateJobPositionDto): Observable<JobPositionDto> {
         let url_ = this.baseUrl + "/api/services/app/jobPosition";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.put<JobPositionDto>(`${url_}/Update?id=${id}`, input);
  }

  // Delete job position by ID
  delete(id: string): Observable<void> {

         let url_ = this.baseUrl + "/api/services/app/jobPosition";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.delete<void>(`${url_}/Delete?Id=${id}`);
  }
}
