import { ProductModel } from "./ProductModel";

export class OrderModel {
	public orderID: number;
	public userName: string;
	public phone: string;
	public userID:string;
	public guestID: string;
	public deliveryAddress: string;
	public totalPrice: number;
	public date: Date;
	public isApprove: boolean;
	public products: ProductModel[];
}
