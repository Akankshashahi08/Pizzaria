import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Order, Product } from '../Models/Product';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  @Input() allOrderProducts: Array<Product> | undefined;
  @Input() productTotal: number | undefined;
  @Output() hideConfirmOrderPopup = new EventEmitter();
  orderModel: Order | undefined

  constructor() {
    this.orderModel = new Order();
    this.orderModel.products = this.allOrderProducts;
    this.orderModel.TotalPrice = this.productTotal;
  }

  ngOnInit(): void {
  }

  modalCancelClick(event: any) {
    debugger;
    this.hideConfirmOrderPopup.emit(0);
  }
}
