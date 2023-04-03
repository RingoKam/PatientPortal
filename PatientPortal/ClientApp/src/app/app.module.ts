// Angular modules
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { NgxDropzoneModule } from 'ngx-dropzone';

// Material Modules
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input'
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { MatButtonModule } from '@angular/material/button';
import {MatDividerModule} from '@angular/material/divider';
import {MatIconModule} from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort'
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

// Custom Components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CsvUploadComponent } from './csv-upload/csv-upload.component';
import { ViewRecordsComponent } from './view-records/view-records.component'
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { UpdatePatientComponent } from './update-patient/update-patient.component'


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CsvUploadComponent,
    ViewRecordsComponent,
    NavBarComponent,
    UpdatePatientComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'upload', component: CsvUploadComponent },
      { path: 'view', component: ViewRecordsComponent } 
    ]),
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatDialogModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatNativeDateModule,
    NgxDropzoneModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
