import { Http, Response } from "@angular/http";
import { Injectable } from "@angular/core";
//reactive map operator import to use observable

import { Observable } from "rxjs/Observable";
import { Product } from "./product";

import 'rxjs/add/operator/map';

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
}