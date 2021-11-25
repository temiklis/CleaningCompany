import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { OrderRequest } from '../../../models/interfaces/OrderRequests/OrderRequest';
import { HelperService } from '../../../services/helper.service';
import { OrderRequestsService } from '../../../services/order-requests.service';

@Component({
  selector: 'app-create-order-request-user-info-modal',
  templateUrl: './create-order-request-user-info-modal.component.html',
  styleUrls: ['./create-order-request-user-info-modal.component.scss']
})
export class CreateOrderRequestUserInfoModalComponent implements OnInit {

  @Input() orderRequest: OrderRequest;
  @Input() totalPrice: number;

  orderRequestForm: FormGroup;

  minDate: NgbDateStruct;

  constructor(public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private orderRequestsService: OrderRequestsService,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.orderRequestForm = this.fb.group({
      Address: this.fb.control(this.orderRequest.Address, [Validators.required]),
      Email: this.fb.control(this.orderRequest.Email, [Validators.required, Validators.email]),
      FIO: this.fb.control(this.orderRequest.FIO, [Validators.required]),
      RequestedDate: this.fb.control(this.orderRequest.RequestedDate, [Validators.required])
    })

    let currentDate = new Date();

    this.minDate = { day: currentDate.getDate(), month: currentDate.getMonth() + 1, year: currentDate.getFullYear() };
  }

  getProductAmount(id: number): number {
    return this.orderRequest.Products.filter(p => p.Id == id).length;
  }

  submit() {
    let orderRequest = this.orderRequestForm.value as OrderRequest;
    let date = orderRequest.RequestedDate as any;
    this.orderRequest =
    {
      ...orderRequest,
      Products: this.orderRequest.Products,
      RequestedDate: date ? new Date(date.year, date.month - 1, date.day) : null
    };

    this.orderRequestsService.createOrderRequest(this.orderRequest).then(result => {
      this.activeModal.close(true);
    }).catch(error => {
      this.activeModal.dismiss(error.error);
    })
  }

}
