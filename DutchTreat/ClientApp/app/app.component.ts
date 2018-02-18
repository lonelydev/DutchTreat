import { Component } from '@angular/core';

@Component({
  selector: 'the-shop',
  templateUrl: "./app.component.html",
  styles: []
})
  // only things exported here are the only stuff used elsewhere
export class AppComponent {
  title = 'Product List';
}
