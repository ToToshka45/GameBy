using Domain.ValueObjects;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstractions;

namespace DataAccess
{
    public class TempDataFactory:IDbInitializer
    {
        private readonly DataContext _dataContext;

        public TempDataFactory(DataContext dataContext) { 
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();

            var playerRole = new Role() { RoleName = "Player", Id = Guid.Parse("dc900ef4-986a-4a74-bff4-30ac6852b66f") };
            _dataContext.Add(playerRole);
            _dataContext.SaveChanges();
            var userTest = new User()
            {
                Email = new UserEmail("string"),
                Login = new UserName("string"),
                Password =
                new UserPassword("string")
            };
            userTest.Roles = new List<UserRole>() { new UserRole() { Role = playerRole, User = userTest } };
            _dataContext.Add(userTest);
            _dataContext.SaveChanges();

        }

    }
}
