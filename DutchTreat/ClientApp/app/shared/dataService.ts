import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
//reactive map operator import to use observable
import { Observable } from "rxjs/Observable";
import { Product } from "./product";

import 'rxjs/add/operator/map';


import { Order, OrderItem } from "./order";
// same result as above, but with a namespace
// but you have to access them with orders.Order and orders.OrderItem
//import * as orders from "./order";

/**
 * Decorating with Injectable to tell angular when you inject
 * DataService, it needs its dependencies.
 * It is a good idea to do this early on, so that as your project
 * grows things that its services rely on are automatically included.
 */
@Injectable()
export class DataService {

  /**
   * The DataService fetches data from our MVC API.
   * To do this it needs to make an http request.
   * This is done using HttpClient which is an angular module
   * that is imported above.
   * However DataService itself is injected into productList component.
   * And we have to make sure that just injecting DataService, would
   * automatically include its dependencies too.
   * We do this by using Injectable.
   * @param http
   */
  constructor(private http: Http) {

  }

  private token: string = "";
  private tokenExpiration: Date;


  public get loginRequired(): boolean {
    return this.token.length == 0 || this.tokenExpiration > new Date();
  }

  public login(creds) {
    return this.http.post("/account/createToken", creds)
      .map(response => {
        let tokenInfo = response.json();
        this.token = tokenInfo.token;
        this.tokenExpiration = tokenInfo.expiration;
        return true;
      });
  }
  /**
   * checkout needs to post to the api with the token information
   * that we just received after login. import Header and use it modify header
   * information during the posting of orders to the api. 
   */
  public checkout() {
    //order number is required on the api
    if (!this.order.orderNumber) {
      this.order.orderNumber = this.order.orderDate.getFullYear().toString()
        + this.order.orderDate.getTime().toString();
    }

    return this.http.post("/api/orders", this.order, {
      headers: new Headers({
        "Authorization": "Bearer " + this.token
      })
    })
      .map(response => {
        this.order = new Order();
        return true;
      });
  }

  /*
  Doing this so that we can share the data that we have
  among the different components
  Services are used for exactly this purpose.
  */
  public products:Product[] = [];

  /**
   * uses http to get the products.
   * Adding typesafety by using return type
   * 
   */
  public loadProducts(): Observable<Product[]> {
    return this.http.get("/api/products")
      .map((result: Response) => this.products = result.json());
  }

  public order: Order = new Order();

  public AddToOrder(product: Product) {    

    let item: OrderItem = this.order.items.find(i => i.productId == product.id);

    if (item) {
      item.quantity++;
    } else {
      item = new OrderItem();

      item.productId = product.id;
      item.productArtist = product.artist;
      item.productCategory = product.category;
      item.productArtId = product.artId;
      item.productTitle = product.title;
      item.productSize = product.size;
      item.unitPrice = product.price;
      item.quantity = 1;
      this.order.items.push(item);
    }
  }
}