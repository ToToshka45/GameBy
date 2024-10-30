using RatingService.Domain.Exceptions;

namespace RatingService.Domain.Models.ValueObjects
{
    public class RatingValue
    {
        public decimal Value { get; }

        private RatingValue(decimal value)
        {
            Validate(value);
            Value = value;
        }

        /// <summary>
        /// Runs validations before creating an instance of the <see cref="RatingValue"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RatingValue CheckAndSetValue(decimal value) => new RatingValue(value);

        private void Validate(decimal value)
        {
            InvalidRatingValueException.ThrowIfInvalid(() => value < 0, "The value is less then 0.");
            InvalidRatingValueException.ThrowIfInvalid(() => value < 5, "The value is more then 5.");
        }
    }
}