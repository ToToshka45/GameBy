using Domain;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Application
{
   
    public class UserTokenService
    {
        private readonly IConnectionMultiplexer _redis;

        public UserTokenService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        private IDatabase Database => _redis.GetDatabase();

        public void AddUserToken(UserToken user)
        {   
            Database.StringSet(user.RefreshToken,
                               JsonSerializer.Serialize(user),
                               expiry: user.ExpirationDate-DateTime.Now);
        }

        public async Task UpdateUserToken(UserToken user, string previousToken) {

            await Database.KeyDeleteAsync(previousToken);
            AddUserToken(user);
        }

        public UserToken? FindUserByRefreshToken(string refreshToken)
        {
            var userData = Database.StringGet(refreshToken);
            if (userData.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<UserToken>(userData!);
        }
    }
}
