import { Component, ViewChild, TemplateRef } from '@angular/core';
import { NgIf } from '@angular/common';
import { ModalDirective } from 'ngx-bootstrap';

import { AuthorizationService } from '../authorization/authorization.service';
import { AccountService } from "../account.service";


@Component({
	selector: 'authorization',
	templateUrl: './authorization.component.html',
	styleUrls: ['./authorization.component.css'],
	providers: [AuthorizationService, AccountService]
})
export class AuthorizationComponent {
	public registrationModel: RegistrationModel;
	public signInModel: SignInModel;
	public authType: string;
	public signInError: any;
	public currentUser: AuthModel;

	constructor(private authorizationService: AuthorizationService, private accountService: AccountService) {
		this.registrationModel = new RegistrationModel();
		this.signInModel = new SignInModel();
		this.currentUser = new AuthModel();
	}
	ngOnInit() {
		this.authType = "Registration";

		this.accountService.isAuthenticated()
			.then(response => {
				this.currentUser = response;
			});
	}

	selectAuthType(type: string) {
		this.authType = type;
	}

	registration(model: RegistrationModel): void {
		this.authorizationService.registration(model)
			.then(response => { })
			.catch(error => {
				document.getElementById('regError').hidden = false;
				document.getElementById('regError').textContent = (error.json() as any).responseStatus.message;
				setTimeout(() => {
					document.getElementById('regError').hidden = true;
				}, 3000);
			})
	}

	signIn(model: SignInModel): void {
		this.authorizationService.signIn(model)
			.then(response => { location.reload(); })
			.catch(error => {
				document.getElementById('signInError').hidden = false;
				document.getElementById('signInError').textContent = (error.json() as any).responseStatus.message;
				setTimeout(() => {
					document.getElementById('signInError').hidden = true;
				}, 3000);
			});
	}

	signOut(): void {
		this.authorizationService.signOut()
			.then(() => {
				window.location.reload();
			})
	}
}


export class RegistrationModel {
	Name: string;
	Email: string;
	Password: string;
	ConfirmPassword: string;
	Country: string;
	Phone: string;
	Year: string;
}


export class SignInModel {
	Name: string;
	Password: string;
}

export class AuthModel {
	isAuthenticated: boolean;
	userName: string;
}