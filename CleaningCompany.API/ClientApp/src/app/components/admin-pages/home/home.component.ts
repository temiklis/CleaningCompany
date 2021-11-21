import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  currentPage: string;
  constructor(private router: Router) {

    router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        this.currentPage = e.urlAfterRedirects || e.url;
      }
    })
  }

  ngOnInit(): void {
  }

}
