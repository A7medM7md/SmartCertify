import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  private baseUrl = 'https://localhost:7261/api';
  constructor(private http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    // after response comes, cast it to <Course[]>
    return this.http.get<Course[]>(this.baseUrl + '/courses');
  }

}
