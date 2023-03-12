import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { newUser, updatedUser, user } from '../interfaces';
import { UserService } from '../user.service';

@Component({
  selector: "users",
  templateUrl: "./users.component.html"
})

export class UsersComponent implements OnInit {
  searchQuery = '';

  showForm = false;

  usersArray: user[] = [];

  // TODO find extension for typos
  sucessMessage = '';

  _selectedUser: user | undefined;
  /*userToDelete!: updatedUser;*/

  _newUser: newUser = {
    name: '',
    surname: '',
    birthDate: new Date(),
    emailAddress: '',
    userTitleId: 1,
    userTypeId: 4,
    isActive: true
  }

  constructor(private http: HttpClient, private userService: UserService) {
  }

  // TODO should be done in the backend
  filterUsers() {
    if (this.searchQuery) {
      this.usersArray = this.usersArray.filter(user => {
        return user.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          user.surname.toLowerCase().includes(this.searchQuery.toLowerCase());
      });
    } else {
      this.getUsers().subscribe(users => {
        this.usersArray = users;
      })
    }
  }

  ngOnInit(): void {
    this.getUsers().subscribe(users => {
      this.usersArray = users;
    })
  }

  // TODO all user related functionality to separate component
  getUsers() {
    return this.http.get<user[]>('https://localhost:7210/api/users');
  }

  detailsUser(selectedUser: user) {
    this._selectedUser = selectedUser;
    this.userService._selectedUser = this._selectedUser;
  }

  async deleteUser(selectedUser: user) {
    if (selectedUser) {
      let userToDelete: updatedUser = {
        id: selectedUser.id,
        name: selectedUser.name,
        surname: selectedUser.surname,
        birthDate: selectedUser.birthDate,
        emailAddress: selectedUser.emailAddress,
        isActive: false,
        userTypeId: 4, // TODO no hard coded
        userTitleId: 1
      };
      console.log('https://localhost:7210/api/users/' + selectedUser.id);

      // TODO should be delete method
      await this.http.put('https://localhost:7210/api/users/' + userToDelete.id, userToDelete).toPromise();
      this.ngOnInit();
    }
  }

  addUser(newUser: newUser) {
    return this.http.post('https://localhost:7210/api/users', newUser);
  }

  createUser() {
    // TODO this.userService.addUser();
    this.addUser(this._newUser).subscribe((data) => {
      this.sucessMessage = "User Added Successfully";
      /* Refresh the user list after successful addition*/
      this.getUsers().subscribe(users => {
        this.usersArray = users;
      });
    }, error => {
      this.sucessMessage = error;
    });
  }

  openUserForm() {
    this.showForm = !this.showForm;
    if (this.showForm) {
      const userForm = document.getElementById('userForm');
      userForm?.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
