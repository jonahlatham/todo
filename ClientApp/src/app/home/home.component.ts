import { Component } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Todo } from '../models/todo';
import { TodoService } from '../services/todoservice';
import { HomeEditDialogComponent } from './home-edit-dialog/home-edit-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  value: string
  todo: Todo
  todos: Array<Todo>

  constructor(private todoService: TodoService, private _snackBar: MatSnackBar, public dialog: MatDialog) { }

  ngOnInit() {
    this.getTodo()
  }

  getTodo() {
    this.todoService.GetTodo().subscribe((res: Todo[]) => {
      this.todos = res
    })
  }

  addTodo() {
    let task = {
      Item: this.value
    }
    if (this.value) {
      this.todoService.PostTodo(task).subscribe((res: Todo) => {
        this.getTodo()
      })
    } else {
      this.openAlertSnackBar()
    }
    this.value = null
  }

  openAlertSnackBar() {
    this._snackBar.open(`You must add a task before adding it to the list`);
  }

  makeComplete(id, item, isComplete) {
    let task = {
      Id: id,
      Item: item,
      IsComplete: !isComplete
    }
    this.todoService.PutTodo(task).subscribe((res) => {
      this.getTodo()
      this.openCompleteSnackBar(item, isComplete)
    })
  }

  openCompleteSnackBar(item: string, isComplete: boolean) {
    this._snackBar.open(`"${item}" was marked ${isComplete ? "not completed" : "completed"}`);
  }

  deleteTodo(id, item) {
    this.todoService.DeleteTodo(id).subscribe((res) => {
      this.getTodo()
      this.openDeleteSnackBar(item)
    })
  }

  openDeleteSnackBar(item: string) {
    this._snackBar.open(`"${item}" was deleted`);
  }

  openEditDialog(id, item, isComplete): void {
    const dialogRef = this.dialog.open(HomeEditDialogComponent, {
      width: '260px',
      data: { item }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.editTodo(id, result, isComplete);
        this.openSuccessfulEditSnackBar(item, result);
      }
    });
  }

  editTodo(id, item, isComplete) {
    const task = {
      Id: id,
      Item: item,
      IsComplete: isComplete
    }
    this.todoService.PutTodo(task).subscribe((res) => {
      this.getTodo()
    })
  }

  openSuccessfulEditSnackBar(item: string, newItem: string) {
    this._snackBar.open(`"${item}" was successfully changed to "${newItem}"`);
  }




  displayId(id) {
    console.log(id)
  }

}
