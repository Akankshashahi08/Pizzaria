import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CartDetailsComponent } from './cart-details/cart-details.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { PizzariaServicesService } from './services/pizzaria-services.service';
import { PizzaCustomizeModalComponent } from './pizza-customize-modal/pizza-customize-modal.component';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { OrderDetailsComponent } from './order-details/order-details.component';



@NgModule({
  declarations: [
    AppComponent,
    ProductDetailsComponent,
    CartDetailsComponent,
    PizzaCustomizeModalComponent,
    OrderDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DialogsModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [PizzariaServicesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
