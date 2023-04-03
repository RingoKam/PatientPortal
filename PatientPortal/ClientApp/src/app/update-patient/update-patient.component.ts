import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Patient } from '../web-api-client';

@Component({
  selector: 'update-patient',
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.css']
})
export class UpdatePatientComponent {
  editableData: Patient;

  constructor(
    public dialogRef: MatDialogRef<UpdatePatientComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Patient,
  ) { }

  ngOnInit(): void {
    this.editableData = JSON.parse(JSON.stringify(this.data))
  }

}
