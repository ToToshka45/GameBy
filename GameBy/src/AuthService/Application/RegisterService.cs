using Application.Dto;
using DataAccess.Abstractions;
using Domain;
using Domain.ValueObjects;
using System.Data;

namespace Application
{
    public class RegisterService
    {
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<Role> _roleRepository;

        public RegisterService(IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> CheckLoginExists(string login)
        {
            var ExistingUser = _userRepository.Search(x => x.Login.Name == login);


            if (ExistingUser == null)
                return true;

            return false;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var ExistingUser = _userRepository.Search(x => x.Email.Email == email);


            if (ExistingUser == null)
                return true;

            return false;
        }

        public async Task<NewUserResultDto> AddNewUser(NewUserDto newUserDto)
        {
            //ToDo Send RabbitMq message

            if (!await CheckLoginExists(newUserDto.UserName))
            {
                return new NewUserResultDto() { IsSuccess = false, ErrorMessage = "Логин занят" };
            }

            if (!await CheckEmailExists(newUserDto.UserEmail))
            {
                return new NewUserResultDto() { IsSuccess = false, ErrorMessage = "Такой email уже есть" };
            }

            var user = new User();

            user.Email = new UserEmail(newUserDto.UserEmail);
            user.Login = new UserName(newUserDto.UserName);
            user.Password = new UserPassword(newUserDto.UserPassword);

            //ToDo AddRole
            var playerRole = await _roleRepository.GetByIdAsync(Guid.Parse("dc900ef4-986a-4a74-bff4-30ac6852b66f"));
                
            user.Roles = new List<UserRole>() { new UserRole() {
                Role = playerRole,
                User=user
            } };

            var newUser = await _userRepository.AddAsync(user);
            if (newUser != null)
                return new NewUserResultDto() { UserName = newUser.Login.Name,
                Id=newUser.Id, IsSuccess=true};

            return new NewUserResultDto() { IsSuccess = false,ErrorMessage="Unknown" };
        }

        //ToDoUpdateRoleMethod
    }
}
