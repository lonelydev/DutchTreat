import { Component } from "@angular/core";

import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";



@Component({
    selector: "login",
    templateUrl: "login.component.html"
})
export class Login {

  constructor(private data: DataService, private router: Router) {

  }

  errorMessage: string = "";
  /*
  * object that represents data on our form.
  * untyped
  */
  public creds = {
    username: "",
    password: ""
  };

  onLogin() {
    // call login service.
    this.data.login(this.creds)
      .subscribe(success => {
        if (success) {
          // if creating token was successful then 
          // decide where to go based on whats in the card.
          // do we have any data items? or not.
          if (this.data.order.items.length == 0) {
            this.router.navigate([""]);
          } else {
            this.router.navigate(["checkout"]);
          }
        }
      }, err => this.errorMessage = "Failed to login")
    
  }
}