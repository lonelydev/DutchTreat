// a different way of loading modules from what we saw in the 
// typescript intro. 
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";


import { AppComponent } from './app.component';
import { ProductList } from "./shop/productList.component";
import { Cart } from "./shop/cart.component";
import { Shop } from "./shop/shop.component";
import { Checkout } from "./checkout/checkout.component";
import { Login } from "./login/login.component";

import { DataService } from "./shared/dataService";
import { HttpModule } from '@angular/http';

import { RouterModule } from "@angular/router";

import { FormsModule } from "@angular/forms";



let routes = [
  //specify pattern or path and then a component 
  { path: "", component: Shop },
  { path: "checkout", component: Checkout },
  { path: "login", component: Login }
];

// essential file, this is the bootstrapping file. 
// there isn't really any code here apart from what's below
// @xyz is called a decorator. 
// tells what we need for the app
// 
@NgModule({
  declarations: [
    AppComponent, // the thing we are going to use on a page
    ProductList,
    Cart, 
    Shop, 
    Checkout,
    Login
  ],
  imports: [
    BrowserModule,  //how to host it on a browser page?! wtf
    HttpModule,
    FormsModule,
    RouterModule.forRoot(routes, {
      // as we aren't really building an spa
      // path is after a hash in the url
      // only enhancing one single page
      useHash: true, 
      enableTracing: false //for debugging routes
    }),
    HttpClientModule //shawn wildermuth initially used httpclientmodule
    // then without warning in the next video, switched to using httpmodule!
  ],
  // things that can be injected into other components
  providers: [
    DataService
  ],
  bootstrap: [AppComponent] //what do i load first. this might load other components
})
export class AppModule { }
