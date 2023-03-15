import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { IUpdatedUser } from "../interfaces/IUpdatedUser";
import { IUser } from "../interfaces/IUser";
import { IUserTitle } from "../interfaces/IUserTitle";
import { IUserType } from "../interfaces/IUserType";
import { UserService } from "../user.service";

@Component({
  selector: 'user-details',
  templateUrl: './user-details.component.html',
})

export class UserdetailsComponent implements OnInit {
  constructor(private userService: UserService, private router: Router) { }

  selectedUser: IUser | undefined;
  selectedUserTitle: IUserTitle | undefined;
  selectedUserType: IUserType | undefined;

  titlesArray: IUserTitle[] = [];
  typesArray: IUserType[] = [];

  _newUser: IUpdatedUser = {
    id: 0,
    name: '',
    surname: '',
    birthDate: new Date(),
    emailAddress: '',
    userTitleId: 1,
    userTypeId: 4,
    isActive: true
  }

  async UpdateUser() {
    if (this.selectedUser) {
      this._newUser.id = this.selectedUser.id;
      await this.userService.putUser(this.selectedUser.id, this._newUser).subscribe();
      this.router.navigate(['/users']);
    }
  }

  async ngOnInit() {
    this.selectedUser = this.userService._selectedUser;

    if (this.selectedUser) {
      await this.userService.getUserTitles().subscribe({
        next: (titles) => {
          this.titlesArray = titles;
        }
      });

      await this.userService.getUserTypes().subscribe({
        next: (types) => {
          this.typesArray = types;
        }
      });

      await this.userService.getUserTitle(this.selectedUser.userTitleId).subscribe({
        next: (title) => {
          this.selectedUserTitle = title;
          console.log(title);
        }
      });

      await this.userService.getUserType(this.selectedUser.userTypeId).subscribe({
        next: (type) => {
          this.selectedUserType = type;
          console.log(type);
        }
      });
    }
  }
}
