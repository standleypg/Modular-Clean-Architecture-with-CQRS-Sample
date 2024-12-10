namespace RetailPortal.Domain.Interfaces.Infrastructure.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}