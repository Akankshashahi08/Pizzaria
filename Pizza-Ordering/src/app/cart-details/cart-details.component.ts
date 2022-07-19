import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../Models/Product';

@Component({
  selector: 'app-cart-details',
  templateUrl: './cart-details.component.html',
  styleUrls: ['./cart-details.component.scss']
})
export class CartDetailsComponent implements OnInit {

  @Input() productInfo: any;
  // @Output() closeFlyout = new EventEmitter();
  productDetails: any[] | undefined

  ngDoCheck(){
    debugger
    if (this.productDetails == undefined) {
      this.productDetails = new Array<Product>();
    }

    this.productDetails?.push(this.productInfo);
  }

  constructor() {
    // debugger
    // if (this.productDetails == undefined) {
    //   this.productDetails = new Array<Product>();
    // }

    // this.productDetails?.push(this.productInfo);
  }

  ngOnInit() {
    //let a = this.cartdata;
    // if (this.productDetails == undefined) {
    //   this.productDetails = new Array<Product>();
    // }

    // this.productDetails?.push(this.productInfo);
  }

  Add(evnt: any) {
    evnt.quantity++
    evnt.total = evnt.Price * evnt.quantity
  }

  remove(evnt: any) {
    evnt.quantity--;
    evnt.total = evnt.Price * evnt.quantity
    // evnt = evnt -1;
  }
}