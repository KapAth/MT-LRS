import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
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
  successMessage = '';

  _selectedUser: user | undefined;

  _newUser: newUser = {
    name: '',
    surname: '',
    birthDate: new Date(),
    emailAddress: '',
    userTitleId: 1,
    userTypeId: 4,
    isActive: true
  }

  constructor(private userService: UserService) { }

  // TODO should be done in the backend
  filterUsers() {
    if (this.searchQuery) {
      this.usersArray = this.usersArray.filter(user => {
        return user.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
          user.surname.toLowerCase().includes(this.searchQuery.toLowerCase());
      });
    } else {
      this.userService.getUsers().subscribe(users => {
        this.usersArray = users;
      })
    }
  }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users => {
      this.usersArray = users;
    })
  }

  detailsUser(selectedUser: user) {
    this._selectedUser = selectedUser;
    this.userService._selectedUser = this._selectedUser;
  }

  async deleteUser(selectedUser: user) {
    if (selectedUser) {
      console.log(selectedUser);
      await this.userService.deleteUser(selectedUser.id).subscribe({
        next: () => {
          /* Refresh the user list after successful deletion*/
          this.userService.getUsers().subscribe({
            next: (users) => {
              this.usersArray = users;
            }
          });
        }
      });
    }
  }

  createUser() {
    this.userService.postUser(this._newUser).subscribe({
      next: () => {
        this.successMessage = "User Added Successfully";
        /* Refresh the user list after successful addition*/
        this.userService.getUsers().subscribe({
          next: (users) => {
            this.usersArray = users;
          },
          error: (error) => {
            this.successMessage = error;
          },
        });
      },
      error: (error) => {
        this.successMessage = error;
      },
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
