import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiPaths, environment } from 'src/environments/environment';
import { IUser } from './interfaces/IUser';
import { INewUser } from './interfaces/INewUser';
import { IUserTitle } from './interfaces/IUserTitle';
import { IUserType } from './interfaces/IUserType';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  //selected user from users table, used to access the user selected in users component to the the user-details component
  _selectedUser: IUser | undefined;

  apiUsers = `${environment.baseUrl}${ApiPaths.Users}`;
  apiUserTitles = `${environment.baseUrl}${ApiPaths.UserTitles}`;
  apiUserTypes = `${environment.baseUrl}${ApiPaths.UserTypes}`;

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<IUser[]>(`${this.apiUsers}`);
  }

  getUsersWithFilter(filter: string) {
    return this.http.get<IUser[]>(`${this.apiUsers}?filter=${filter}`);
  }

  putUser(id: number, userToUpdate: IUser) {
    return this.http.put(`${this.apiUsers}/${id}`, userToUpdate);
  }

  postUser(newUser: INewUser) {
    return this.http.post(`${this.apiUsers}`, newUser);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.apiUsers}/${id}`);
  }

  getUserTitles() {
    return this.http.get<IUserTitle[]>(`${this.apiUserTitles}`);
  }

  getUserTypes() {
    return this.http.get<IUserType[]>(`${this.apiUserTypes}`);
  }
}
