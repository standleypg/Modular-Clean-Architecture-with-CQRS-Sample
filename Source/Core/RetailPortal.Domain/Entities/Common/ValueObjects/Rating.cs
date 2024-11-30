using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Core.Entities.Common.ValueObjects;

public class Rating : ValueObject
{
    public int Value { get; }

    private Rating(int value)
    {
        this.Value = value;
    }

    public static Rating Create(int value)
    {
        return new Rating(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}