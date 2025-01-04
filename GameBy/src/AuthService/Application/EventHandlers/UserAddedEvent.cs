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
        public Guid UserId { get; }

        public UserAddedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
