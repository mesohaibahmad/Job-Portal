import { Component, OnInit } from '@angular/core';
import { CandidateService } from './candidate.service';
import { CandidateDto } from './candidate.model';
import { ConfirmationService, MessageService , LazyLoadEvent} from 'primeng/api';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
    providers: [ConfirmationService, MessageService]
})
export class CandidateListComponent implements OnInit {
  candidates: CandidateDto[] = [];
    totalCount = 0;

  pageIndex = 0;
  pageSize = 10;
  keyword = '';
  loading = false;
 dialogVisible = false;
 sorting = 'fullName ASC';
  selectedCandidate: CandidateDto | null = null;


  constructor(private candidateService: CandidateService,
     private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

 

  ngOnInit(): void {
    this.loadCandidates();
  }

    loadCandidates(event?: LazyLoadEvent) {
    this.loading = true;

    const request = {
  skipCount: event?.first ?? this.pageIndex * this.pageSize,
  maxResultCount: event?.rows ?? this.pageSize,
  sorting: this.sorting,
  keyword: this.keyword
};

    this.candidateService.getAll(request).subscribe(res => {
      this.candidates = res.result.items;
      this.totalCount = res.result.totalCount;
      this.loading = false;
    });
  }
  openEditDialog(candidate: CandidateDto): void {
  
  this.selectedCandidate = { ...candidate };
  this.dialogVisible = true;
  }

  downloadResume(path: string | undefined) {
    if (!path) return;

    const fullPath = `/Resumes/${path}`;
    const a = document.createElement('a');
    a.href = fullPath;
    a.download = path.split('/').pop()!;
    a.click();
  }

  deleteCandidate(id: number) {
    if (!confirm('Are you sure you want to delete this candidate?')) return;

    this.candidateService.delete(id).subscribe(() => {
      this.loadCandidates();
    });
  }



  onSearch() {
    this.pageIndex = 0;
    this.loadCandidates();
  }

  onDialogSaved(): void {
this.dialogVisible = false;
this.loadCandidates();
}

onDialogClose(): void {
this.dialogVisible = false;

}


    openCreateModal() {
    this.dialogVisible = true;
    this.selectedCandidate = null;
  }




confirmDelete(id: number) {
  this.confirmationService.confirm({
    message: 'Are you sure you want to delete this candidate?',
    accept: () => {
      this.deleteCandidate(id);
    }
  });
}

}
