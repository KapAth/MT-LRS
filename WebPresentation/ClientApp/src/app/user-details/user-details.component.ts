import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { IUpdatedUser } from "../interfaces/IUpdatedUser";
import { IUser } from "../interfaces/IUser";
import { UserService } from "../user.service";

@Component({
  selector: 'user-details',
  templateUrl: './user-details.component.html',
})

export class UserdetailsComponent implements OnInit {
  constructor(private userService: UserService, private router: Router) { }

  selectedUser: IUser | undefined;

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

  ngOnInit(): void {
    this.selectedUser = this.userService._selectedUser;
  }
}
