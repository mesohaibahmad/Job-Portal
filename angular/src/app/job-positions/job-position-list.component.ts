
import { Component, OnInit } from '@angular/core';
import { JobPositionService } from './job-position.service';
import {  JobPositionDto } from './job-position.models';
import { LazyLoadEvent } from 'primeng/api';

@Component({
selector: 'app-job-position-list',
templateUrl: './job-position-list.component.html',
})

export class JobPositionListComponent implements OnInit {
jobPositions: JobPositionDto[] = [];
totalCount = 0;
loading = false;
pageSize = 10;
pageIndex = 0;
keyword = '';
sorting = 'title ASC';
dialogVisible = false;
selectedJob: JobPositionDto | null = null;

constructor(private jobPositionService: JobPositionService) {}


ngOnInit(): void {
this.loadData();
}

loadData(event?: LazyLoadEvent): void {
this.loading = true;

const request = {
  skipCount: event?.first ?? this.pageIndex * this.pageSize,
  maxResultCount: event?.rows ?? this.pageSize,
  sorting: this.sorting,
  keyword: this.keyword
};

this.jobPositionService.getAllJobs(request).subscribe(resp => {
  
  this.jobPositions = resp.result.items;
  this.totalCount = resp.result.totalCount;
  this.loading = false;
});
}

openCreateDialog(): void {
this.selectedJob = null;
this.dialogVisible = true;
}

openEditDialog(job: JobPositionDto): void {
this.selectedJob = { ...job };
this.dialogVisible = true;
}

deleteJob(id: string): void {
if (confirm('Are you sure you want to delete this job position?')) {
this.jobPositionService.delete(id).subscribe(() => {
this.loadData();
});
}
}

onDialogClose(): void {
this.dialogVisible = false;

}

onDialogSaved(): void {
this.dialogVisible = false;
this.loadData();
}

onSearch(): void {
this.pageIndex = 0;
this.loadData();
}
}







