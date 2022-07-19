import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Menu } from '../Models/Product';

@Injectable({
  providedIn: 'root'
})


export class PizzariaServicesService {

  baseURL = environment.apiBaseUrl 

 // private http: HttpClient;
  //private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;


  constructor(public http: HttpClient) {
      //this.http = http;
    //  this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  }



  getPizzaSize() {
    return this.http.get<any>(this.baseURL+ "/api/size/all")
  }

  getCategory() {
    return this.http.get<any>(this.baseURL+ "/api/category/all")
  }

  getCrust() {
    return this.http.get<any>(this.baseURL+ "/api/crust/all")
  }

  getToppings(){
    return this.http.get<any>(this.baseURL+ "/api/topping/all")
  }

  getMenu(){
    return this.http.get<any>(this.baseURL+ "/api/menu/all")
  }
}
