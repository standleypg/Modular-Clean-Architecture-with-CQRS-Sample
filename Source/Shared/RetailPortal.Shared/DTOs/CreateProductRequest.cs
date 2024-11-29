namespace RetailPortal.Shared.DTOs;

public record CreateProductRequest(
    string Name,
    string Description,
    PriceRequest Price,
    int Quantity
);

public record PriceRequest(decimal Value, string Currency);