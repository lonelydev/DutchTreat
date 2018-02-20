import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Product } from "../shared/product";

//helps with intellisense
@Component({
  selector: "product-list",
  templateUrl: "productList.component.html",
  styleUrls: [ "productList.component.css" ] //add a style file
})
  /* OnInit interface says, once you are ready for it, call my method
   * to do all my bookkeeping that angular has setup.
  */
export class ProductList implements OnInit {
  /* Declare products as an array of Product imported above
  */
  public products: Product[];

  /**
 * injecting a service to a class using dependency injection
 * but in a very OO way! Great!
 * using the private keyword to create a private data member to
 * point to the DataService
 * To ensure the injection works, we have to go the app.module.ts
 * and ensure we import it there. 
 * @param data
 */
  constructor(private data: DataService) {
    this.products = data.products;
  }

  /**
   * The method ngOnInit implemented by this class
   */
  ngOnInit(): void {
    this.data.loadProducts()
      .subscribe(() => this.products = this.data.products);
  }  
}