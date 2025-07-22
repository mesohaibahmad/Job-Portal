import { Component, EventEmitter, Input, OnChanges, Output , SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CandidateDto, CreateUpdateCandidateDto } from './candidate.model';
import { CandidateService } from './candidate.service';
import { JobPositionService } from '../job-positions/job-position.service';
import { DropdownModule } from '@node_modules/primeng/dropdown/dropdown';

@Component({
  selector: 'app-candidate-create-edit',
  templateUrl: './candidate-create-edit.component.html'
})
export class CandidateCreateEditComponent  implements OnChanges{
  @Input() visible = false;
  @Input() data: CandidateDto | null = null;
  @Output() onClose = new EventEmitter<boolean>();
  @Output() onSaved = new EventEmitter<void>();

   ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.form.patchValue({
      
        fullName: this.data.fullName,
        email: this.data.email,
        jobPositionId: this.data.jobPositionId,

      });
       this.selectedFile = null;
      this.form.get('resume')?.reset();

    
    }
  }
  form: FormGroup;
  jobPositions: any[] = [];
  selectedFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private candidateService: CandidateService,
    private jobPositionService: JobPositionService
  ) {

  }

  ngOnInit() {
    this.jobPositionService.getAllJobs().subscribe(res => {
      this.jobPositions = res.result.items;
    });

       this.form = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      jobPositionId: ['', Validators.required],
     
    });


  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file && ['application/pdf', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', 'image/jpeg', 'image/png'].includes(file.type)) {
      this.selectedFile = file;
    } else {
      alert('Invalid file type');
    }
  }

  save() {
    const formData = new FormData();
    if(this.data?.id)
    {
    formData.append('Id', this.data.id);
    }
    formData.append('FullName', this.form.value.fullName);
    formData.append('Email', this.form.value.email);
    formData.append('JobPositionId', this.form.value.jobPositionId);
    if (this.selectedFile) {
      formData.append('ResumeFile', this.selectedFile);
    }
    else{
       alert('Resume file is required.');
    return;
    }

const request = this.data?.id
  ? this.candidateService.update( formData)
  : this.candidateService.create(formData);
  
    request.subscribe(() => {
      this.onSaved.emit();
      this.form.reset();
      this.selectedFile = null;
    });
  }

  onCancel(): void {
this.visible = false;
this.form.reset();
this.onClose.emit();
}
}
