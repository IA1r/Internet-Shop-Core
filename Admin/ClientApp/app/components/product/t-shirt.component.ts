import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Http } from '@angular/http';
import { FileUploader } from 'ng2-file-upload';

import { ProductModel } from "../models/ProductModel";
import { ProductComponent } from "./product.component";

@Component({
	selector: 't-shirt',
	templateUrl: './t-shirt.component.html',
	styleUrls: ['../product/product.component.css']
})

export class TShirtComponent {

	@Input('product') public product: ProductModel;
	@Input('uploader') public uploader: FileUploader;

	@Output() updateProduct = new EventEmitter<ProductModel>();
	@Output() cancelUpdate = new EventEmitter();
	@Output() updateImage = new EventEmitter();
	@Output() setIsEdit = new EventEmitter<boolean>();

	public IsEdit: boolean;

	constructor() { }

	update(product: ProductModel) {
		this.updateProduct.emit(product);
		this.IsEdit = false;
	}

	cancel() {
		this.cancelUpdate.emit();
		this.IsEdit = false;
	}

	updateImg() {
		this.updateImage.emit();
	}

	isEdit(value: boolean) {
		this.IsEdit = value;
		this.setIsEdit.emit(value);
	}
}
