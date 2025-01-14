
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class UserAddedEventHandler:INotificationHandler<UserAddedEvent>
    {
        private readonly RabbitService _rabbitMqService;

        public UserAddedEventHandler(RabbitService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public  Task Handle(UserAddedEvent notification, CancellationToken cancellationToken)
        {
            _rabbitMqService.SendMessage("rating_service_user_registered", notification.UserId.ToString());
            _rabbitMqService.SendMessage("gamer_service_user_registered", notification.UserId.ToString());
            // Your logic for handling the user added event
            Console.WriteLine($"User added: {notification.UserId}");
            

            return Task.CompletedTask;
        }
    }
}
