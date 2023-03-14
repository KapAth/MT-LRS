import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiPaths, environment } from 'src/environments/environment';
import { newUser, user } from './interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  //selected user from users table, used to send a user between users component and user-details component
  _selectedUser: user | undefined;

  apiUsers = `${environment.baseUrl}${ApiPaths.Users}`;

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<user[]>(`${this.apiUsers}`);
  }

  getUsersWithFilter(filter: string) {
    console.log(`${this.apiUsers}?filter=${filter}`)
    return this.http.get<user[]>(`${this.apiUsers}?filter=${filter}`)
  }

  postUser(newUser: newUser) {
    return this.http.post(`${this.apiUsers}`, newUser);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.apiUsers}/${id}`);
  }
}
