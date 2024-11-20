using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Core.Entities.Common.ValueObjects;

public class AverageRating : ValueObject
{
    private int TotalRating { get; }
    private int NumberOfRatings { get; }
    public decimal Value => this.NumberOfRatings == 0 ? 0 : (decimal)this.TotalRating / this.NumberOfRatings;

    private AverageRating(int totalRating, int numberOfRatings)
    {
        this.TotalRating = totalRating;
        this.NumberOfRatings = numberOfRatings;
    }

    public static Result<AverageRating> Create(int totalRating = 0, int numberOfRatings = 0)
    {
        if (totalRating < 0)
            return Result<AverageRating>.Failure("Total rating must be greater than or equal to 0");

        return numberOfRatings < 0
            ? Result<AverageRating>.Failure("Number of ratings must be greater than or equal to 0")
            : Result<AverageRating>.Success(new AverageRating(totalRating, numberOfRatings));
    }

    public Result<AverageRating> AddRating(int rating)
    {
        return rating is < 0 or > 5
            ? Result<AverageRating>.Failure("Rating must be between 0 and 5")
            : Result<AverageRating>.Success(new AverageRating(this.TotalRating + rating, this.NumberOfRatings + 1));
    }

    public override string ToString() => this.Value.ToString("0.0");

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.TotalRating;
        yield return this.NumberOfRatings;
    }
}