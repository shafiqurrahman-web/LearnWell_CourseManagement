
using LearnWell.CourseManagement.Application.Abstractions.Clock;

namespace LearnWell.CourseManagement.Infrastructure.Clock;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
