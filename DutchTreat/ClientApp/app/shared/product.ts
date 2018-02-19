/*
* An interface that represents a product. Need to look like the Product
* returned by the API. This will help us set up some type safety
* Use Postman, retrive products json. Copy on product object representation.
* goto json2ts.com, paste your json. 
*/
export interface Product {
  id: number;
  category: string;
  size: string;
  price: number;
  title: string;
  artDescription: string;
  artDating: string;
  artId: string;
  artist: string;
  artistBirthDate: Date;
  artistDeathDate: Date;
  artistNationality: string;
}