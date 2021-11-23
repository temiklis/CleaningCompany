import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  currentPage: string;
  constructor(private router: Router) {

  }

  ngOnInit(): void {
    this.currentPage = this.router.url;
  }

  changePage(url: string) {
    this.currentPage = `/admin${url}`;
    this.router.navigate([this.currentPage]);
  }

}
