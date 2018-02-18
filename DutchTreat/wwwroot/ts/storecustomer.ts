// classes are typically Pascalcased like .net
export class StoreCustomer {

  // creates private fields of the same name as params in the constructor
  constructor(private firstName:string, private lastName:string) {
  }

  // public protected and private just like OO
  // types aren't required but can be inferred based on assignment
  // or explicitly specified using :type syntax
  public visits: number = 0;
  private ourName: string;

  // can restrict what the type of the parameter is
  // also can set the return type
  public showName() {
    alert(this.firstName + " " + this.lastName);
  }

  set name(val) {
    this.ourName = val;
  }

  get name() {
    return this.ourName;
  }

}

