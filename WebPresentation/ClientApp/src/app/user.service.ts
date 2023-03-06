import { Injectable } from '@angular/core';
import { user } from './interfaces';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  _selectedUser: user | undefined;
} // TODO what is this for?
