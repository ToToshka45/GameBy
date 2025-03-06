using Application.Dto;
using Application.EventHandlers;
using DataAccess.Abstractions;
using Domain;
using Domain.ValueObjects;
using MediatR;
using System.Data;

namespace Application
{
    public class RegisterService
    {
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<Role> _roleRepository;

        private readonly IMediator _mediator;

        public RegisterService(IRepository<User> userRepository,
            IRepository<Role> roleRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mediator = mediator;
        }

        public async Task<bool> CheckLoginExists(string login)
        {
            var ExistingUser =await _userRepository.Search(x => x.Login.Name == login);


            if (ExistingUser.Count()==0)
                return true;

            return false;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var ExistingUser =await _userRepository.Search(x => x.Email.Value == email);


            if (ExistingUser.Count() == 0)
                return true;

            return false;
        }

        public async Task<NewUserResultDto> AddNewUser(NewUserDto newUserDto)
        {
            //ToDo Send RabbitMq message

            if (!await CheckLoginExists(newUserDto.Username))
            {
                return new NewUserResultDto() { IsSuccess = false, ErrorMessage = "Логин занят" };
            }

            if (!await CheckEmailExists(newUserDto.Email))
            {
                return new NewUserResultDto() { IsSuccess = false, ErrorMessage = "Такой email уже есть" };
            }

            var user = new User();

            user.Email = new UserEmail(newUserDto.Email);
            user.Login = new UserName(newUserDto.Username);
            user.Password = new UserPassword(newUserDto.Password);

            //ToDo AddRole
            var playerRole = await _roleRepository.GetByIdAsync(1);
            var orgRole = await _roleRepository.GetByIdAsync(2);

            user.Roles = new List<UserRole>() { new UserRole() {
                Role = playerRole,
                User=user
            }, new UserRole() {Role=orgRole,User=user } };

            var newUser = await _userRepository.AddAsync(user);
            if (newUser != null)
            {

                var userAddedEvent = new UserAddedEvent(newUser.Id,newUser.Login.Name);
                await _mediator.Publish(userAddedEvent);
                return new NewUserResultDto()
                {
                    UserName = newUser.Login.Name,
                    Id = newUser.Id,
                    IsSuccess = true
                };
            }

            return new NewUserResultDto() { IsSuccess = false, ErrorMessage="Unknown" };
        }

        //ToDoUpdateRoleMethod
    }
}
