using System.Linq.Expressions;

namespace RatingService.Domain.Exceptions;

public class InvalidRatingValueException : Exception
{
    public InvalidRatingValueException(string message) : base(message) { }

    /// <summary>
    /// Asserts a given Rating Value and if it doesn`t meet the criteria - the Exception will be thrown.
    /// </summary>
    public static void ThrowIfInvalid(Func<bool> predicate, string errorMessage)
    {
        var assert = predicate();
        if (assert is true) throw new InvalidRatingValueException(errorMessage);
    }
}
