using BeachOrganizer.Application.Common.Interfaces.Services;

namespace BeachOrganizer.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}