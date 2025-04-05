using Domain.ValueObjects;
using Domain;
using DataAccess.Abstractions;

namespace DataAccess;

public class TempDataFactory : IDbInitializer
{
    private readonly DataContext _dataContext;

    public TempDataFactory(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void InitializeDb()
    {
        _dataContext.Database.EnsureDeleted();
        _dataContext.Database.EnsureCreated();

        if (!_dataContext.Roles.Any())
        {
            var gamerRole = new Role() { RoleName = "Gamer", Id = 1 };
            _dataContext.Add(gamerRole);
            var organizerRole = new Role() { RoleName = "Organizer", Id = 2 };
            _dataContext.Add(organizerRole);
            _dataContext.SaveChanges();
        }
        if (!_dataContext.Users.Any())
        {
            IEnumerable<User> users = [
                new()
                {
                    Email = new UserEmail("user1@gameby.com"),
                    Login = new UserName("user1"),
                    Password = new UserPassword("user1"),
                    Roles = [new() { RoleId = 1, UserId = 1 }, new() { RoleId = 2, UserId = 1 }]
                },
                new()
                {
                    Email = new UserEmail("user2@gameby.com"),
                    Login = new UserName("user2"),
                    Password = new UserPassword("user2"),
                    Roles = [new() { RoleId = 1, UserId = 1 }, new() { RoleId = 2, UserId = 1 }]
                },
                new()
                {
                    Email = new UserEmail("user3@gameby.com"),
                    Login = new UserName("user3"),
                    Password = new UserPassword("user3"),
                    Roles = [new() { RoleId = 1, UserId = 1 }, new() { RoleId = 2, UserId = 1 }]
                }
            ];
            _dataContext.AddRange(users);
            _dataContext.SaveChanges();
        }
    }
}
