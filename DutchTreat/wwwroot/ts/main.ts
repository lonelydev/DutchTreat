// import only the types that you need. from whatever is exposed
// if you need more then {} will contain comma separated types
// from a certain file path without the extension. it is implicitly understood to be
// a typescript file.
import {
  StoreCustomer
} from "./storecustomer"


let shopper = new StoreCustomer("Shawn", "Wildermuth");
shopper.showName();

//compile/transpiling typescript. 