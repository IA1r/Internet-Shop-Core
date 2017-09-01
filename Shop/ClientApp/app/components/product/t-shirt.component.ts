import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Http } from '@angular/http';


import { ProductComponent } from "./product.component";
import { ProductModel } from "./products.component";

@Component({
	selector: 't-shirt',
	templateUrl: './t-shirt.component.html',
	styleUrls: ['../product/product.component.css']
})

export class TShirtComponent {

	@Input('product') public product: ProductModel;
	@Output() addProduct = new EventEmitter();

	public IsEdit: boolean;

	constructor() { }

	addProductToCart() {
		this.addProduct.emit();
	}

}
