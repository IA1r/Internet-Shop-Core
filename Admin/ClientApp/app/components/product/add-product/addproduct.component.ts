
import { Component, Inject, OnInit } from '@angular/core';
import { FileUploader } from "ng2-file-upload";

import { ProductService } from "../product.service";
import { ProductModel } from "../../models/ProductModel";
import { ProductTypeModel } from "../../models/ProductTypeModel";


@Component({
	selector: 'addproduct',
	templateUrl: './addproduct.component.html',
	providers: [ProductService],
	styleUrls: ['./addproduct.component.css']
})

export class AddProductComponent implements OnInit {
	public productTypes: ProductTypeModel[];
	public product: ProductModel;
	public selectedType: number;
	public uploader: FileUploader;
	public success: boolean;
	public successUpload: boolean;

	constructor(private productServie: ProductService) { }

	ngOnInit() {
		this.productServie.getProductTypes()
			.then(response => {
				this.productTypes = response.types;
			})
	}

	initDictionaryFields(typeID: number) {
		this.productServie.initDictionaryFields(typeID)
			.then(response => {
				this.product = response.product;
			})
	}

	addProductToDB() {
		this.product.productTypeID = this.selectedType;
		this.productServie.addProductToDB(this.product)
			.then(response => {
				this.product.productID = response.producID;
				this.success = response.responseStatus.success;
				this.uploader = new FileUploader({ url: '/api/ProductAPI/ImageUpload/' + this.product.productID });
			})
	}

	updateImage() {
		this.uploader.uploadAll();
		this.uploader.onCompleteItem = (response: any) => {
			this.product.characteristic.Image = JSON.parse(response._xhr.response).imageID;
			this.successUpload = JSON.parse(response._xhr.response).responseStatus.success;
		}
	}
}
