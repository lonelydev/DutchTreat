
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