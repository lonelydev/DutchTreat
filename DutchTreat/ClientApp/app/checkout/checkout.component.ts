import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from "@angular/router";

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {

  constructor(public data: DataService, private router: Router) {
  }

  errorMessage: string = "";

  onCheckout() {
    this.data.checkout()
      .subscribe(success => {
        if (success) {
          this.router.navigate([""]);
        }
      },err => this.errorMessage = "Failed to save order");
  }
}