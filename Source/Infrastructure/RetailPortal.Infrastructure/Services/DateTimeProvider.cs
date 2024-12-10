using RetailPortal.Domain.Interfaces.Infrastructure.Services;

namespace RetailPortal.Infrastructure.Services;

public sealed class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}