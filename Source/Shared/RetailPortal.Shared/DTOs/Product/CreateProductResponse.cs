namespace RetailPortal.Shared.DTOs.Product;

public record CreateProductResponse
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    string? ImageUrl,
    Guid? CategoryId,
    Guid? SellerId
);