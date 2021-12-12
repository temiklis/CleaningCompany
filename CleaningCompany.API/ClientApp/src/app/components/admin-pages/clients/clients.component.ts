import { Component, OnInit } from '@angular/core';
import { Client } from '../../../models/interfaces/Clients/Client';
import { ClientSearchParams } from '../../../models/interfaces/Clients/ClientSearchParams';
import { XPagination } from '../../../models/interfaces/X-Pagination';
import { ClientsService } from '../../../services/clients.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {

  clients: Client[];

  searchParams: ClientSearchParams;
  defaultSearchParams: ClientSearchParams = {
    Name: null,
    Email: null,
    PageNumber: 1,
    PageSize: 10
  }

  pagination: XPagination;

  constructor(private clientsService: ClientsService) { }

  ngOnInit(): void {
    this.searchParams = { ...this.defaultSearchParams };
    this.getClients();
  }

  getClients() {
    this.clientsService.getAllClients(this.searchParams).then(clients => {
      this.clients = clients;
      this.pagination = JSON.parse(localStorage.getItem('X-Pagination'));
    })
  }

  goToClientDetails(id: number) {

  }

  updateResults(page: number) {
    this.searchParams.PageNumber = page;
    this.getClients();
  }

  resetFilters() {
    this.searchParams = { ...this.defaultSearchParams };
    this.getClients();
  }
}
