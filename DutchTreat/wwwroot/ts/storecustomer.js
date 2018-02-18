"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// classes are typically Pascalcased like .net
var StoreCustomer = /** @class */ (function () {
    // creates private fields of the same name as params in the constructor
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        // public protected and private just like OO
        // types aren't required but can be inferred based on assignment
        // or explicitly specified using :type syntax
        this.visits = 0;
    }
    // can restrict what the type of the parameter is
    // also can set the return type
    StoreCustomer.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
    };
    Object.defineProperty(StoreCustomer.prototype, "name", {
        get: function () {
            return this.ourName;
        },
        set: function (val) {
            this.ourName = val;
        },
        enumerable: true,
        configurable: true
    });
    return StoreCustomer;
}());
exports.StoreCustomer = StoreCustomer;
//# sourceMappingURL=storecustomer.js.map