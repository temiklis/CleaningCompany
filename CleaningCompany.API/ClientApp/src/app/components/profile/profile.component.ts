import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  roles: string[];

  constructor(private userService: UsersService) { }

  ngOnInit(): void {
    this.getRoles();
  }

  getRoles() {
    this.userService.getRoles().then(roles => {
      this.roles = roles;
    })
  }

}
