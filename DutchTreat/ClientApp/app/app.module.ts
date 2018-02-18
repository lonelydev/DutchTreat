// a different way of loading modules from what we saw in the 
// typescript intro. 
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { ProductList } from "./shop/productList.component";

// essential file, this is the bootstrapping file. 
// there isn't really any code here apart from what's below
// @xyz is called a decorator. 
// tells what we need for the app
// 
@NgModule({
  declarations: [
    AppComponent, // the thing we are going to use on a page
    ProductList
  ],
  imports: [
    BrowserModule  //how to host it on a browser page?! wtf
  ],
  providers: [],
  bootstrap: [AppComponent] //what do i load first. this might load other components
})
export class AppModule { }
