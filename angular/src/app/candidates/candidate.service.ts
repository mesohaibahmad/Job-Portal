import { Injectable, Inject , Optional} from '@angular/core';
import { HttpClient , HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CandidateDto, CreateUpdateCandidateDto } from './candidate.model';
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {

   private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

   constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
          this.http = http;
          this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
      }

  getAll(input?: any): Observable<any> {

     let url_ = this.baseUrl + "/api/services/app/Candidate";
        url_ = url_.replace(/[?&]$/, "");

    const params = new HttpParams({ fromObject: input || {} });
    return this.http.get<{ items: CandidateDto[], totalCount: number }>(
      `${url_}/GetAll`, { params }
    );
  }


  
  create(formData: FormData): Observable<CandidateDto> {

      let url_ = this.baseUrl + "/api/services/app/Candidate";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.post<CandidateDto>(`${url_}/Create`, formData);
  }

  delete(id: number): Observable<void> {

      let url_ = this.baseUrl + "/api/services/app/Candidate";
        url_ = url_.replace(/[?&]$/, "");
    return this.http.delete<void>(`${url_}/Delete?Id=${id}`);
  }
   update( input: FormData): Observable<CandidateDto> {
           let url_ = this.baseUrl + "/api/services/app/candidate";
          url_ = url_.replace(/[?&]$/, "");
      return this.http.put<CandidateDto>(`${url_}/Update`, input);
    }
  
}
