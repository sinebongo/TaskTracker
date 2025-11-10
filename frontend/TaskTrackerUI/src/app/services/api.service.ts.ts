import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task';
import { environment } from '../../environments/environments';

@Injectable({
    providedIn: 'root'
})
export class ApiServiceTs {
    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    private getHeaders(): HttpHeaders {
        return new HttpHeaders({
            'Content-Type': 'application/json'
        });
    }

    getAllTasks(q?: string, sort?: string): Observable<Task[]> {
        let params = new HttpParams();
        if (q) {
            params = params.set('q', q);
        }
        if (sort) {
            params = params.set('sort', sort);
        }
        return this.http.get<Task[]>(this.apiUrl, { params });
    }

    getTaskById(id: number): Observable<Task> {
        return this.http.get<Task>(`${this.apiUrl}/${id}`);
    }

    createTask(task: Task): Observable<Task> {
        return this.http.post<Task>(this.apiUrl, task, { headers: this.getHeaders() });
    }

    updateTask(id: number, task: Task): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, task, { headers: this.getHeaders() });
    }

    deleteTask(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}