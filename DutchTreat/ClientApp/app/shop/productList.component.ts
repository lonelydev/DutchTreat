import { Component } from "@angular/core";

//helps with intellisense
@Component({
  selector: "product-list",
  templateUrl: "productList.component.html",
  styleUrls: []
})
export class ProductList {
  public products = [{
    title: "First Product",
    price: 19.99
  },
  {
    title: "Second Product",
    price: 15.99
  },
  {
    title: "Third Product",
    price: 22.99
  },
  {
    title: "Fourth Product",
    price: 11.29
  }];
}