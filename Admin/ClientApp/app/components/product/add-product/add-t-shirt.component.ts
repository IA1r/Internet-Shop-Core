﻿
import { Component, Inject, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileUploader } from "ng2-file-upload";
import { ProductTypeModel } from "../../models/ProductTypeModel";
import { ProductModel } from "../../models/ProductModel";


@Component({
	selector: 'add-t-shirt',
	templateUrl: './add-t-shirt.component.html',
	styleUrls: ['../../product/product.component.css']
})

export class AddTShirtComponent {
	@Input('product') public product: ProductModel;
	@Input('uploader') public uploader: FileUploader;
	@Input('success') public success: boolean;
	@Input('successUpload') public successUpload: boolean;

	@Output() addProductToDB = new EventEmitter();
	@Output() updateImage = new EventEmitter();	

	constructor() { }


	addProduct() {
		this.addProductToDB.emit();
	}

	updImage() {
		this.updateImage.emit();
	}
}
