using RatingService.Domain.Enums;

namespace RatingService.Infrastructure.Utilities;

internal static class Utilities
{
    /// <summary>
    /// Returns all the categories from the enum-class <see cref="EventCategory"/>.
    /// </summary>
    /// <returns></returns>
    internal static EventCategory[] GetCategories()
    {
        return (EventCategory[])Enum.GetValues(typeof(EventCategory));
    }
}
