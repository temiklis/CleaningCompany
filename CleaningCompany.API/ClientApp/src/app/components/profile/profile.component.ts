import { Component, OnInit } from '@angular/core';
import { AuthorizeService, IUser } from '../../services/authorization/authorize.service';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  roles: string[];
  user: IUser;

  constructor(private userService: UsersService,
    private authorizeService: AuthorizeService) { }

  ngOnInit(): void {
    this.authorizeService.getUser().subscribe(user => {
      if (user) {
        this.user = user;
      }
    })
    this.getRoles();
  }

  getRoles() {
    this.userService.getRoles().then(roles => {
      this.roles = roles;
    })
  }

}
