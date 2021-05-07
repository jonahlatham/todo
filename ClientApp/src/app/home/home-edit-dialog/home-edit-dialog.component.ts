import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material';
import { Todo } from 'src/app/models/todo';
import { HomeComponent } from '../home.component';

@Component({
  selector: 'app-home-edit-dialog',
  templateUrl: './home-edit-dialog.component.html',
  styleUrls: ['../home.component.scss']
})
export class HomeEditDialogComponent implements OnInit {

  oldItem: string

  constructor(
    public dialogRef: MatDialogRef<HomeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Todo,
    private _snackBar: MatSnackBar) { }

  ngOnInit() { 
   this.oldItem = this.data.item
  }

  onNoClick(): void {
    this.dialogRef.close()
    this.openCancelSnackBar()
  }

  openCancelSnackBar() {
    this._snackBar.open(`Edit was canceled`);
  }

}
