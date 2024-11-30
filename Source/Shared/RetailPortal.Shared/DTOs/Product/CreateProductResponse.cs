namespace RetailPortal.Shared.DTOs.Product;

public record CreateProductResponse
(
    Guid Id,
    string Name,
    string Description,
    Price Price,
    int Quantity,
    string? ImageUrl,
    Guid? CategoryId,
    Guid? SellerId
);

public record Price(decimal Value, string Currency);