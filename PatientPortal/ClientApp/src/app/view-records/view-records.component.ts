import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { Patient, PatientClient, SortOrder } from '../web-api-client';
import { BehaviorSubject, Observable, Subscription, combineLatest, debounceTime, filter, finalize, startWith, switchMap, tap } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { UpdatePatientComponent } from '../update-patient/update-patient.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { resolveErrorMsg } from '../error-util';

@Component({
  selector: 'app-view-records',
  templateUrl: './view-records.component.html',
  styleUrls: ['./view-records.component.css']
})
export class ViewRecordsComponent implements OnInit, AfterViewInit, OnDestroy {

  dataSource : BehaviorSubject<[]> = new BehaviorSubject([]);
  nameFilter$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  loading$: BehaviorSubject<boolean> = new BehaviorSubject(false); 
  sortState$: BehaviorSubject<Sort> = new BehaviorSubject(<Sort> {});

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'gender', 'birthDay'];
  
  obs: Subscription

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _patientClient: PatientClient) {
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.obs = combineLatest(
      [
        this.paginator.page.pipe(startWith(null)), 
        this.nameFilter$.pipe(startWith(null)),
        this.sortState$.pipe(startWith(null)),
      ]
    )
    .pipe(
      debounceTime(500),
      tap(() => this.loading$.next(true)),
      switchMap(() => this.fetchData()),
    ).subscribe({
      next: () => this.loading$.next(false),
      error: (err) => window.alert("An Error has occured")
    })
  }

  ngOnDestroy() {
    this.obs.unsubscribe();
  }

  fetchData() {
    const sortValue = this.sortState$.value
    const sortDirection = sortValue.direction == "asc" ? SortOrder.Ascending : SortOrder.Descending;
    return this._patientClient.patientRecords(sortValue.active, sortDirection, this.nameFilter$.value, this.paginator?.pageIndex ?? 0, this.paginator?.pageSize ?? 10)
      .pipe(
        tap(collection => {
          this.dataSource.next(<any>collection.data);
          this.paginator.pageIndex = (collection?.page ?? 0 );
          this.paginator.pageSize = collection.perPage;
          this.paginator.length = collection.length;
        })
      )
  }

  openDialog(record: Patient) {
    console.log(record)
    const dialogRef = this._dialog.open(UpdatePatientComponent, { data: record });
    dialogRef.afterClosed()
    .pipe(
      filter(result => result),
      tap(() => this.loading$.next(true)),
      switchMap((result) => {
        return this._patientClient.updatePatientRecord(result)
      }),
      switchMap(() => {
        return this.fetchData()
      }),
    )
    .subscribe({
      next: (result) => {
        this._snackBar.open("Record updated", "👍", { duration: 3000 })
        this.loading$.next(false)
      },
      error: (err) => {
        const msg = resolveErrorMsg(err, "Update failed")
        this._snackBar.open(msg, "❌");
        this.loading$.next(false)
      }
    })
  }
}
