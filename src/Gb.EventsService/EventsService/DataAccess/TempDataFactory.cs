using DataAccess.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TempDataFactory:IDbInitializer
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
            var EventTemp1 = new Event()
            {
                Id=1,
                CreationDate = DateTime.Now.ToUniversalTime(),
                EventCategory=Common.EventCategory.Poker,
                EventDate=DateTime.Now.AddDays(2).ToUniversalTime(),
                EventStatus=Constants.EventStatus.Announced,
                Description="Покер техасский холдэм. Мин ставка 20. Банк каждого 1000",
                Title="Игра в покер",
                ParticipantMinimum=3,
                ParticipantLimit=6,
                Location= "Малая Ордынка ул., 37, Москва, 115184",
                IsClosedParticipation=false,
                MaxDuration=5,
                OrganizerId=1,
                EventMembers=new System.Collections.ObjectModel.Collection<Participant>()
                {
                    new Participant()
                    {
                        UserId = 2,
                        Role = Constants.EventUserRole.Player,
                        EventId = 1
                    },
                    new Participant()
                    {
                        UserId = 3,
                        Role = Constants.EventUserRole.Player,
                        EventId = 1
                    },
                }
                
            };
            _dataContext.Add(EventTemp1);
            _dataContext.SaveChanges();

            var EventTemp2 = new Event()
            {
                Id = 2,
                CreationDate = DateTime.Now.ToUniversalTime(),
                EventCategory = Common.EventCategory.Mafia,
                EventDate = DateTime.Now.AddDays(1).ToUniversalTime(),
                EventStatus = Constants.EventStatus.Announced,
                Description = "Мафия с коммисаром и врачом",
                Title = "Мафия",
                ParticipantMinimum = 6,
                ParticipantLimit = 9,
                Location = "ул. Моховая, 7, Москва, 119019",
                IsClosedParticipation = false,
                MaxDuration = 4,
                OrganizerId = 4,
                EventMembers = new System.Collections.ObjectModel.Collection<Participant>()
                {
                    new Participant()
                    {
                        UserId = 5,
                        Role = Constants.EventUserRole.Player,
                        EventId = 2
                    },
                    new Participant()
                    {
                        UserId = 6,
                        Role = Constants.EventUserRole.Player,
                        EventId = 2
                    }
                }

            };

            _dataContext.Add(EventTemp2);
            _dataContext.SaveChanges();
            //var EventMember1 = ;

        }
    }
}
