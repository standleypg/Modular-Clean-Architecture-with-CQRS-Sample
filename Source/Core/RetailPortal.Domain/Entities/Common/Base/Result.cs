namespace RetailPortal.Core.Entities.Common.Base;

public class Result<T>
{
    public T Value { get; }
    public string? Error { get; }
    public bool IsSuccess => this.Error == null;

    private Result(T value)
    {
        this.Value = value;
        this.Error = null;
    }

    private Result(string error)
    {
        this.Value = default!;
        this.Error = error;
    }

    public static Result<T> Success(T value) => new (value);
    public static Result<T> Failure(string error) => new (error);
}