using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class UserAddedEvent : INotification
    {
        public int UserId { get; }

        public string UserName { get; }

        public UserAddedEvent(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
