import { Component, Input, OnInit, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JobPositionDto } from './job-position.models';
import { JobPositionService } from './job-position.service';
import { title } from 'process';

@Component({
selector: 'app-job-position-edit-dialog',
templateUrl: './job-position-edit-dialog.component.html',
})
export class JobPositionEditDialogComponent implements OnChanges {
@Input() visible = false;
@Input() data: JobPositionDto | null = null;
@Output() onClose = new EventEmitter<void>();
@Output() onSaved = new EventEmitter<void>();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.form.patchValue({
        id: this.data.id,
        title: this.data.title,
        description: this.data.description,
        isActive: this.data.isActive
      });
    }
  }

form!: FormGroup;

constructor(private fb: FormBuilder, private jobPositionService: JobPositionService) {}

ngOnInit(): void {
this.form = this.fb.group({
title: [this.data?.title || '', Validators.required],
description: [this.data?.description || '', Validators.required],
isActive: [this.data?.isActive ?? true]
});
}

onSave(): void {
if (this.form.invalid) return;


const payload = {
  ...this.data,
  ...this.form.value
};

const request = this.data?.id
  ? this.jobPositionService.update(this.data.id, payload)
  : this.jobPositionService.create(payload);

request.subscribe(() => {
  this.onSaved.emit();
  this.visible = false;
});
}

onCancel(): void {
this.visible = false;
this.onClose.emit();
this.form.reset();
}
}