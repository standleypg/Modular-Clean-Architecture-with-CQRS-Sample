using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Core.Entities.Common.ValueObjects;

public sealed class Price: ValueObject
{
    public decimal Value { get; }
    public string Currency { get; }

    private Price(decimal value, string currency)
    {
        this.Value = value;
        this.Currency = currency;
    }

    public static Result<Price> Create(decimal value, string currency)
    {
        if (value < 0)
            return  Result<Price>.Failure("Price must be greater than or equal to 0");

        if (string.IsNullOrWhiteSpace(currency))
            return  Result<Price>.Failure("Currency must not be empty");

        return Result<Price>.Success(new Price(value, currency));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
        yield return this.Currency;
    }
}