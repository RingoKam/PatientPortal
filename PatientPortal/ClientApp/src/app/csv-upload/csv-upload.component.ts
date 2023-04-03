import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FileParameter, PatientClient } from '../web-api-client';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-csv-upload',
  templateUrl: './csv-upload.component.html',
  styleUrls: ['./csv-upload.component.css']
})
export class CsvUploadComponent {
  fileToUpload: File[] = [];
  loading$ : BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(
    private patientClient: PatientClient
    ) { }

  onFileSelected(event: any) {
    console.log(event)
    this.fileToUpload.push(event.addedFiles[0]);
  }

  onRemove(event: File) {
    this.fileToUpload.splice(this.fileToUpload.indexOf(event), 1);
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if(this.fileToUpload) {
      const formData = this.fileToUpload.map(f => <FileParameter>{ data: f, fileName: f.name })
      this.loading$.next(true);
      this.patientClient.fileUpload(formData)
        .subscribe(
          {
            error: (er) => { window.alert("Failed to upload CSV") },
            next: () => {
              this.loading$.next(false);
              this.fileToUpload = [];
            }
          }
        );
    }
  }
}
