using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Core.Entities.Common.ValueObjects;

public class Rating : ValueObject
{
    public int Value { get; }

    private Rating(int value)
    {
        this.Value = value;
    }

    public static Result<Rating> Create(int value)
    {
        return value is >= 0 and <= 5
            ? Result<Rating>.Failure("Rating must be between 0 and 5")
            : Result<Rating>.Success(new Rating(value));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}