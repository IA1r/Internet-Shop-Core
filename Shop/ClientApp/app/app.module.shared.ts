import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ModalModule, BsDropdownModule } from 'ngx-bootstrap';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { CustomFormsModule } from 'ng2-validation';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './components/app/app.component';
import { ProductsComponent } from './components/product/products.component';
import { ProductService } from './components/product/product.service';
import { AuthorizationComponent } from './components/account/authorization/authorization.component'
import { AuthorizationService } from './components/account/authorization/authorization.service';
import { ProductComponent } from './components/product/product.component';
import { ShoppingCartService } from './components/shopping-cart/shopping-cart.service';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { AccountService } from "./components/account/account.service";
import { OrderComponent } from "./components/order/order.component";
import { OrderService } from "./components/order/order.service";
import { PCGameComponent } from "./components/product/pc-game.component";
import { MangaComponent } from "./components/product/manga.component";
import { TShirtComponent } from "./components/product/t-shirt.component";
import { SSDComponent } from "./components/product/ssd.component";
import { RAMComponent } from "./components/product/ram.component";


export const sharedConfig: NgModule = {
	bootstrap: [AppComponent],
	declarations: [
		AppComponent,
		ProductsComponent,
		AuthorizationComponent,
		ProductComponent,
		ShoppingCartComponent,
		OrderComponent,
		PCGameComponent,
		MangaComponent,
		TShirtComponent,
		SSDComponent,
		RAMComponent
	],
	imports: [
		FormsModule,
		BrowserModule,
		CustomFormsModule,
		NgxPaginationModule,
		HttpModule,
		RouterModule.forRoot([
			{ path: 'products', component: ProductsComponent },
			{ path: '', redirectTo: 'products', pathMatch: 'full' },
			{ path: 'registration', component: AuthorizationComponent },
			{ path: 'product/:id', component: ProductComponent },
			{ path: 'shopping-cart', component: ShoppingCartComponent },
			{ path: 'orders', component: OrderComponent }
		]),
		ModalModule.forRoot(),
		BsDropdownModule.forRoot()
	],
	providers: [ProductService, AuthorizationService, ShoppingCartService, AccountService, OrderService]
};
