import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../Models/Product';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  @Input() product: any;
  @Output() addToCartEmit = new EventEmitter();
  @Output() addToppingsToCartEmit = new EventEmitter();

  closeResult = '';
  showPopup = false;
  constructor() { }
  
  ngOnInit(): void {
  }

  AddToCartInfo(prod: Product) {
    this.addToCartEmit.emit(prod);
  }

  AddToCart(event: any){
    this.showPopup = false;
    this.addToCartEmit.emit(event);
  }

  openToppings() {
    this.showPopup = true;
  }

  closeToppings() {
    this.showPopup = false;
  }
}
