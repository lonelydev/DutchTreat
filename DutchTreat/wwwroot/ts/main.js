"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// import only the types that you need. from whatever is exposed
// if you need more then {} will contain comma separated types
// from a certain file path without the extension. it is implicitly understood to be
// a typescript file.
var storecustomer_1 = require("./storecustomer");
var shopper = new storecustomer_1.StoreCustomer("Shawn", "Wildermuth");
shopper.showName();
//compile/transpiling typescript.  
//# sourceMappingURL=main.js.map