// a different way of loading modules from what we saw in the 
// typescript intro. 
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";


import { AppComponent } from './app.component';
import { ProductList } from "./shop/productList.component";
import { DataService } from "./shared/dataService";
import { HttpModule } from '@angular/http';

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
    BrowserModule,  //how to host it on a browser page?! wtf
    HttpModule, 
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
