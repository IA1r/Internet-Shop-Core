
import { Component, Inject, OnInit } from '@angular/core';

import { ProductModel } from "../product/products.component";
import { OrderService } from "./order.service";



@Component({
	selector: 'order',
	templateUrl: './order.component.html',
	providers: [OrderService],
	styleUrls: ['./order.component.css']
})

export class OrderComponent implements OnInit {

	public order: OrderModel;
	public orderList: OrderModel[];
	constructor(private orderService: OrderService) { }


	ngOnInit() {
		this.getOrderList();
	}

	getOrder(orderID: number) {
		this.orderService.getOrder(orderID)
			.then(response => {
				this.order = response.order;
			});
	}

	getOrderList() {
		this.orderService.getOrderList()
			.then(response => {
				if (response != null)
					this.orderList = response.orderList;
			});
	}
}


export class OrderModel {
	public userName: string;
	public phone: string;
	public deliveryAddress: string;
	public totalPrice: number;
	public date: Date;
	public isApprove: boolean;
	public products: ProductModel[];
}
