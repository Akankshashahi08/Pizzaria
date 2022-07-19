import { Component, EventEmitter, Output } from '@angular/core';
import { Menu, Product } from './Models/Product';
import { PizzariaServicesService } from './services/pizzaria-services.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  title = 'Pizza-Ordering';
  //@Output() cartdata = new EventEmitter();
  //cartdata: any | undefined;
  menu: any | undefined;
  allCartProduct: Array<Product> | undefined;
  cartTotal = 0;
  showConfirmOrderPopup = false;

  constructor(public service: PizzariaServicesService) {
    
    if (this.allCartProduct == undefined) {
      this.allCartProduct = new Array<Product>();
    }
    const data =localStorage.getItem("AllProductCart");
    if (data != null && data !== '') {
      this.allCartProduct = JSON.parse(atob(data));
    }

    var total =localStorage.getItem("CartTotal")
    if (total != null && total !== '') {    
     this.cartTotal = JSON.parse(atob(total));
    }
    this.getMenu();
  }

  getMenu() {
    this.service.getMenu().subscribe((result: any) => {
      if (result) {
        this.menu = result;
      }
    }, (error: any) => {
      console.log(error);
    });
  }

  addToCartListener(prod: any) {
    debugger;
 
    if (prod.isCustomized) {
      prod.total = prod.priceAfterCustomization;
    } else {
      prod.total = prod.price;
    }
    this.allCartProduct?.push(prod);
    this.updateCartTotal();
    localStorage.setItem("AllProductCart", btoa(JSON.stringify(this.allCartProduct)));
  }


  UpdateQuantity(evnt: any, product: any) {
    let currentSelectedQuantity = Number(evnt.target.value);

    if (product.isCustomized) {
      product.total = product.priceAfterCustomization * currentSelectedQuantity;
    } else {
      product.total = product.price * currentSelectedQuantity;
    }
    this.updateItem(product);
    this.updateCartTotal();
  }

  updateItem(product: any) {
    if (this.allCartProduct != undefined) {
      let index = this.allCartProduct?.indexOf(product);
      this.allCartProduct[index] = product
    }
  }
  DeleteCart(product:any){
    this.allCartProduct?.forEach((value,index)=>{
      if(value==product) 
      {
        this.allCartProduct?.splice(index,1);
      }
  });
  localStorage.setItem("AllProductCart", btoa(JSON.stringify(this.allCartProduct)));
  this.updateCartTotal();
  }

  updateCartTotal() {
    debugger;
    this.cartTotal = 0;
    let priceArray = this.allCartProduct?.map(m => m.total);
    priceArray?.forEach((obj) => {
      this.cartTotal = Number(obj) + Number(this.cartTotal);
    });
    localStorage.setItem("CartTotal", btoa(JSON.stringify(this.cartTotal)));
  }



  ConfirmOrder() {
    if(this.allCartProduct && this.allCartProduct?.length > 0){
    this.showConfirmOrderPopup = true;
    }
  }
  
  hideConfirmOrderPopup(event: any) {
    this.allCartProduct = [];
    this.cartTotal = 0;
    this.showConfirmOrderPopup = false;
    localStorage.clear()
  }
}


