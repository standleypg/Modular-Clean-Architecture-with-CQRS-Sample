declare namespace RetailPortal.Shared {
	export enum iIssProvider {
		Google = 0,
		Azure = 1
	}
}
declare namespace RetailPortal.Shared.DTOs.Auth {
	export interface iAuthResponse {
		email: string;
		firstName: string;
		id: string;
		lastName: string;
		token: string;
	}
	export interface iLoginRequest {
		email: string;
		password: string;
	}
	export interface iRegisterRequest {
		email: string;
		firstName: string;
		lastName: string;
		password: string;
	}
}
declare namespace RetailPortal.Shared.DTOs.Common {
	export interface iODataResponse<T> {
		count: number;
		nextPage: string;
		value: T;
	}
}
declare namespace RetailPortal.Shared.DTOs.Product {
	export interface iCreateProductRequest {
		description: string;
		name: string;
		price: RetailPortal.Shared.DTOs.Product.iPriceRequest;
		quantity: number;
	}
	export interface iPrice {
		currency: string;
		value: number;
	}
	export interface iPriceRequest {
		currency: string;
		value: number;
	}
	export interface iProductResponse {
		categoryId: string;
		description: string;
		id: string;
		imageUrl: string;
		name: string;
		price: RetailPortal.Shared.DTOs.Product.iPrice;
		quantity: number;
		sellerId: string;
	}
}