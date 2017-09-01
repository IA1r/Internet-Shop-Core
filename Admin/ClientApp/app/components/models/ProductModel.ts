export class ProductModel {
	productID: number;
	productTypeID: number;
	cartContentID: number;
	type: string;
	characteristic: { [key: string]: string };
}