
declare namespace RetailPortal.Shared.DTOs.Product {
	interface Price {
		currency: string;
		value: number;
	}
	interface ProductResponse {
		categoryId: string;
		description: string;
		id: string;
		imageUrl: string;
		name: string;
		price: RetailPortal.Shared.DTOs.Product.Price;
		quantity: number;
		sellerId: string;
	}
}
declare namespace System {
	interface Guid {
		allBitsSet: string;
		variant: number;
		version: number;
	}
}
