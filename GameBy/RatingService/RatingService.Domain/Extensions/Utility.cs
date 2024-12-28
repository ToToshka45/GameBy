namespace RatingService.Common.Models.Extensions;

public static class Extensions
{
    public static TEnum TryParseOrDefault<TEnum>(this string name, TEnum defaultEnum) where TEnum : struct, Enum =>
        Enum.TryParse<TEnum>(name, out var result) ? result : defaultEnum;
}
