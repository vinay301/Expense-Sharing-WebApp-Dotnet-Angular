import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseApiUrl : string = environment.baseApiUrl;
constructor(private http: HttpClient) { }

getAllUsers() : Observable<User[]>{
  return this.http.get<User[]>(this.baseApiUrl + '/api/Users/GetAllUsers');
}

}
