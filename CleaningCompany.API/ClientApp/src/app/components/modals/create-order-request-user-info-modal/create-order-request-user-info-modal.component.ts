import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderRequest } from '../../../models/interfaces/OrderRequests/OrderRequest';
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

  constructor(public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private orderRequestsService: OrderRequestsService) { }

  ngOnInit(): void {
    this.orderRequestForm = this.fb.group({
      Address: this.fb.control(this.orderRequest.Address, [Validators.required]),
      Email: this.fb.control(this.orderRequest.Email, [Validators.required, Validators.email]),
      FIO: this.fb.control(this.orderRequest.FIO, [Validators.required])
    })
  }

  getProductAmount(id: number): number {
    return this.orderRequest.Products.filter(p => p.Id == id).length;
  }

  submit() {
    let orderRequest = this.orderRequestForm.value as OrderRequest;
    this.orderRequest = { ...orderRequest, Products: this.orderRequest.Products };
    this.orderRequestsService.createOrderRequest(this.orderRequest).then(result => {
      this.activeModal.close(true);
    }).catch(error => {
      this.activeModal.dismiss(error.error);
    })
  }

}
