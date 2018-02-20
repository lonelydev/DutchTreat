import * as _ from "lodash";

/* interfaces cannot be instantiated
 * so we use classes.
 * 2 classes in one file.
 * Order class also has attributes initialized with
 * Default values
 */
export class Order {
  orderId: number;
  orderDate: Date = new Date();
  orderNumber: string;
  items: Array<OrderItem> = new Array<OrderItem>();

  /*
  * instead of making just a value use get to make it a property
  * get tells ts that this is a read only property.
  * like c# properties and their gets and sets.
  * gets are implemented like methods. but accessed like a field.
  * set is also done in a similar way.
  */
  get subtotal(): number {
    // use lodash to sum up thte totals
    // _.map() creates a collection of unitPrice * quantity for every 
    // item. then _.sum() is going to add it all.
    return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
  };
}

export class OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  productId: number;
  productCategory: string;
  productSize: string;
  productTitle: string;
  productArtist: string;
  productArtId: string;
}