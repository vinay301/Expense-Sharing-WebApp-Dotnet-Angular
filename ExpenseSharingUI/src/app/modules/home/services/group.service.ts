import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Group } from '../../../core/models/group.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

baseApiUrl : string = environment.baseApiUrl;
constructor(private http : HttpClient) { }

getAllGroups() : Observable<Group[]>{
  return this.http.get<{ $values: Group[] }>(this.baseApiUrl + '/api/group/GetAllGroups').pipe(
    map(response => response.$values)
  );
  // return this.http.get<Group[]>(this.baseApiUrl + '/api/group/GetAllGroups')
}

getGroupById(groupId : string) : Observable<Group>{
  return this.http.get<Group>(this.baseApiUrl + `/api/group/GetGroupById/${groupId}`)
}

}
