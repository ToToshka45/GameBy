using RatingService.Domain.Enums;

namespace RatingService.Infrastructure.Utilities;

internal static class Utilities
{
    /// <summary>
    /// Returns all the categories from the enum-class <see cref="Category"/>.
    /// </summary>
    /// <returns></returns>
    internal static Category[] GetCategories()
    {
        return (Category[])Enum.GetValues(typeof(Category));
    }
}
