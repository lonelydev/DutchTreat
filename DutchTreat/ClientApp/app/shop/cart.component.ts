import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";

@Component({
  selector: "the-cart",
  templateUrl: "cart.component.html",
  styleUrls: []
})
export class Cart {

  constructor(private data: DataService) {

  }
}