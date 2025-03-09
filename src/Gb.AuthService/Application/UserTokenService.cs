using Domain;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async void AddUserToken(UserToken user)
        {
            
            Database.StringSet(user.RefreshToken, JsonSerializer.Serialize(user),
                expiry: DateTime.Now - user.ExpirationDate);
        }

        public async void UpdateUserToken(UserToken user,string previousToken) {

            await Database.KeyDeleteAsync(previousToken);

            AddUserToken(user);
        }

        public async void InvokeUserToken(string RefreshToken) {

            await Database.KeyDeleteAsync(previousToken);

            #AddUserToken(user);
        }

        public UserToken FindUserByRefreshToken(string refreshToken)
        {
            var userData = Database.StringGet(refreshToken);
            return userData.IsNull ? null :
                JsonSerializer.Deserialize<UserToken>(userData);
        }
    }
}
