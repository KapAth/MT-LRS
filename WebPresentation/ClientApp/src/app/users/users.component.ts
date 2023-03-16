import { Component, OnInit } from "@angular/core";
import { INewUser } from "../interfaces/INewUser";
import { IUser } from "../interfaces/IUser";
import { IUserTitle } from "../interfaces/IUserTitle";
import { IUserType } from "../interfaces/IUserType";
import { UserService } from '../user.service';

@Component({
  selector: "users",
  templateUrl: "./users.component.html"
})

export class UsersComponent implements OnInit {
  searchQuery = '';

  emailPattern = '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

  showForm = false;

  usersArray: IUser[] = [];
  titlesArray: IUserTitle[] = [];
  typesArray: IUserType[] = [];

  successMessage = '';
  errorMessage = '';

  _selectedUser: IUser | undefined;

  _newUser: INewUser = {
    name: '',
    surname: '',
    birthDate: new Date(),
    emailAddress: '',
    userTitleId: 0,
    userTypeId: 0,
    isActive: true
  }

  constructor(private userService: UserService) { }

  filterUsers() {
    if (this.searchQuery) {
      this.userService.getUsersWithFilter(this.searchQuery).subscribe(users => {
        this.usersArray = users;
      })
    } else {
      this.userService.getUsers().subscribe(users => {
        this.usersArray = users;
      })
    }
  }

  async ngOnInit(): Promise<void> {
    await this.userService.getUsers().subscribe(users => {
      this.usersArray = users;
    })
  }

  detailsUser(selectedUser: IUser) {
    this._selectedUser = selectedUser;
    this.userService._selectedUser = this._selectedUser;
  }

  async deleteUser(selectedUser: IUser) {
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
          error: () => {
            this.errorMessage = "Please ensure all required fields are filled out accurately and try again";
          },
        });
      },
      error: () => {
        this.errorMessage = "Please ensure all required fields are filled out accurately and try again";
      },
    });
  }

  async openUserForm() {
    this.showForm = !this.showForm;
    if (this.showForm) {
      const userForm = document.getElementById('userForm');
      userForm?.scrollIntoView({ behavior: 'smooth', block: 'start' });

      await this.userService.getUserTitles().subscribe({
        next: (titles) => {
          this.titlesArray = titles;
        }
      });

      await this.userService.getUserTypes().subscribe({
        next: (types) => {
          this.typesArray = types;
          console.log(this.typesArray);
        }
      });
    }
  }
}
