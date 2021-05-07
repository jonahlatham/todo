import { HttpClient } from "@angular/common/http";
import { Component, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class TodoService {
    constructor(private http: HttpClient) { }

    public GetTodo(): Observable<any> {
        return this.http.get(`/todo`);
    }
    public PostTodo(todo) {
        return this.http.post(`/todo`, todo);
    }
    public PutTodo(todo) {
        return this.http.put(`/todo`, todo);
    }
    public DeleteTodo(id) {
        return this.http.delete(`/todo/?id=${id}`);
    }
}