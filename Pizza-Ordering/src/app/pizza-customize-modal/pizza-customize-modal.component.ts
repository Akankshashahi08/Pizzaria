import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Crust, Product, Size, Topping } from '../Models/Product';
import { PizzariaServicesService } from '../services/pizzaria-services.service';

@Component({
  selector: 'pizza-customize-modal',
  templateUrl: './pizza-customize-modal.component.html',
  styleUrls: ['./pizza-customize-modal.component.scss']
})
export class PizzaCustomizeModalComponent implements OnInit {
  public showPopup = false;
  @Output() newItemEvent = new EventEmitter();
  @Output() prodDetails = new EventEmitter();
  @Input() product: Product | undefined;
  toppings: Array<Topping> | undefined;
  sizes: Array<Size> | undefined;
  crusts: Array<Crust> | undefined;
  selectedToppings: Array<Topping> | undefined;
  selectedSize: Size | undefined;
  selectedCrust: Crust | undefined;

  constructor(public service: PizzariaServicesService) {
    this.selectedToppings = new Array<Topping>();
    this.getToppings();
    this.getSize();
    this.GetCrust();
  }

  ngOnInit(): void {
    if (this.product != undefined) {
      this.product.isCustomized = true;
      this.product.priceAfterCustomization = this.product?.price;
      this.selectedSize = this.product.size;
      this.selectedCrust = this.product.crust;
    }
  }

  getToppings() {
    this.service.getToppings().subscribe((result: any) => {
      if (result) {
        this.toppings = result;
      }
    }, (error: any) => {
      console.log(error);
    });
  }

  getSize() {
    this.service.getPizzaSize().subscribe((result: any) => {
      if (result) {
        this.sizes = result;
      }
    }, (error: any) => {
      console.log(error);
    });
  }

  GetCrust() {
    this.service.getCrust().subscribe((result: any) => {
      if (result) {
        let productBasePrice = this.product?.price;
        this.crusts = result;
        this.crusts?.forEach((obj) => {
          switch (obj.crustId) {
            case 1: //Hand Tossed
              obj.price = Number(productBasePrice);
              break;
            case 2: //100% Wheat Thin Crust
              obj.price = Number(productBasePrice) + 50;
              break;
            case 3: //Cheese Brust
              obj.price = Number(productBasePrice) + 100;
              break;
            case 5: //Classic Hand Tossed
              obj.price = Number(productBasePrice) + 90;
              break;
          }
        });

        // TODO: update this.selectedCrust price value
        var updateSelectedCrust = this.crusts?.find(a => a.crustId == this.selectedCrust?.crustId);
        if (this.selectedCrust != undefined) {
          this.selectedCrust.price = updateSelectedCrust?.price;
        }
      }
    }, (error: any) => {
      console.log(error);
    });
  }


  calculateCrust() {
    let productBasePrice = this.product?.price;
    this.crusts?.forEach((obj) => {
      switch (obj.crustId) {
        case 1: //Hand Tossed
          obj.price = Number(productBasePrice);
          break;
        case 2: //100% Wheat Thin Crust
          obj.price = Number(productBasePrice) + 50;
          break;
        case 3: //Cheese Brust
          obj.price = Number(productBasePrice) + 100;
          break;
        case 5: //Classic Hand Tossed
          obj.price = Number(productBasePrice) + 90;
          break;
      }
    });
  }

  modalCancelClick(event: any) {
    this.newItemEvent.emit(0);
  }

  onSizeChange(size: any) {
    this.selectedSize = size;
    this.calculateCrust();
    switch (this.selectedSize?.sizeId) {
      case 2: //Regular minus 100
        this.crusts?.forEach((obj) => {
          obj.price = Number(obj?.price) - 100;
        });
        break;
      case 3: // Medium minus 100
        this.GetCrust();
        break;
      case 4: //Large add 100
        this.crusts?.forEach((obj) => {
          obj.price = Number(obj?.price) + 100;
        });
        break;
    }

    // TODO: update this.selectedCrust price value
    var updateSelectedCrust = this.crusts?.find(a => a.crustId == this.selectedCrust?.crustId);
    if (this.selectedCrust != undefined) {
      this.selectedCrust.price = updateSelectedCrust?.price;
    }

    this.calculatePizzaPrice();
  }

  onCrustChange(crust: any) {
    this.selectedCrust = crust;
    this.calculatePizzaPrice();
  }

  onSelectTopping(event: any, topping: any) {
    if (event.currentTarget.checked) {
      this.selectedToppings?.push(topping);
    } else {
      var filtered = this.selectedToppings?.filter(function (obj, index, arr) {
        return obj?.toppingId != topping.toppingId;
      });
      this.selectedToppings = filtered;
    }

    if (this.product != null) {
      this.product.pizzaToppings = this.selectedToppings;
      this.calculatePizzaPrice();
    }
  }

  AddToCartInfo() {
    if (this.product != undefined) {
      this.product.crustId = this.selectedCrust?.crustId;
      this.product.crust = this.selectedCrust;

      this.product.sizeId = this.selectedSize?.sizeId;
      this.product.size = this.selectedSize;
    }
    
    this.calculatePizzaPrice();
    this.prodDetails.emit(this.product);
  }

  calculatePizzaPrice() {
    if (this.product != undefined) {
      let totalPrice = 0;
      let toppingPriceArray = this.product?.pizzaToppings?.map(m => m.price);
      toppingPriceArray?.forEach((obj) => {
        totalPrice = Number(obj) + totalPrice;
      });

      totalPrice = totalPrice + Number(this.selectedCrust?.price);
      this.product.priceAfterCustomization = totalPrice;

      let toppingNameArray = this.product?.pizzaToppings?.map(m => m.name);
      this.product.pizzaSelectedToppingNames = toppingNameArray?.join(', ');
    }
  }

  public showDialog(state: any) {
    if (state === 0) {
      this.showPopup = false;
    } else {
      this.showPopup = true;
    }
  }
}
