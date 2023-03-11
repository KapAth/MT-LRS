import { HttpClient } from "@angular/common/http";
import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { Router, RouterModule } from '@angular/router';
import { updatedUser, user } from "../interfaces";
import { UserService } from "../user.service";

@Component({
  selector: 'user-details',
  templateUrl: './user-details.component.html',
})

// TODO PascalCase naming on components
export class userdetailsComponent implements OnInit {

  constructor(private userService: UserService, private http: HttpClient, private router: Router) { }

  selectedUser: user | undefined;

  _newUser: updatedUser = {
    id:0,
    name: '',
    surname: '',
    birthDate: new Date(),
    emailAddress: '',
    userTitleId: 1,
    userTypeId: 4,
    isActive: true
  }
    // TODO single spacing

  async UpdateUser() {
    if (this.selectedUser) {
      this._newUser.id = this.selectedUser.id;
      console.log(this._newUser);

      // TODO you should call a service and the service should use the http client. also base url should be configurable per environment.
      await this.http.put('https://localhost:7210/api/users/' + this.selectedUser.id, this._newUser).toPromise();
      this.router.navigate(['']);

    }
  }

  ngOnInit(): void {
   this.selectedUser = this.userService._selectedUser;
    console.log(this.selectedUser); // TODO no console logs on finished code


  }



}
