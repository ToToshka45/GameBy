using Bogus;
using System.ComponentModel.DataAnnotations;

namespace RatingService.Common.CommonServices;

public sealed class FakeDataProvider
{
    public static IEnumerable<UserCreatedTestEvent> ProvideUserCreatedEventTestData(int usersCount)
    {
        // HashSet to ensure uniqueness
        HashSet<int> uniqueIntegers = [];
        Randomizer randomizer = new Randomizer();

        int minValue = 1;
        int maxValue = 100;
        var faker = new Faker();

        List<UserCreatedTestEvent> users = [];

        while (uniqueIntegers.Count < usersCount)
        {
            uniqueIntegers.Add(randomizer.Int(minValue, maxValue));
        }

        foreach (var userId in uniqueIntegers)
        {
            users.Add(new(userId, faker.Internet.UserName()));
        }

        return users;
    }

    public record UserCreatedTestEvent(int ExternalUserId, string UserName);
}
