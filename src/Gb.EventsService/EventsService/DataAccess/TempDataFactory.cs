using DataAccess.Abstractions;
using Domain;

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
        if (_dataContext.Events.Any()) return;

        IEnumerable<Event> events = [
            new()
            {
                Id = 1,
                CreationDate = DateTime.Now.ToUniversalTime(),
                EventCategory = Common.EventCategory.Poker,
                EventDate = DateTime.Now.AddDays(2).ToUniversalTime(),
                EventStatus = Constants.EventStatus.Announced,
                Description = "Покер техасский холдэм. Мин ставка 20. Банк каждого 1000",
                Title = "Игра в покер",
                MinParticipants = 3,
                MaxParticipants = 6,
                Location = "Малая Ордынка ул., 37, Москва, 115184",
                //IsClosedParticipation=false,
                //MaxDuration=5,
                OrganizerId = 1,
                Participants = [
                    new Participant()
                    {
                        UserId = 2,
                        Username = "DoomGuy",
                        //Role = Constants.EventUserRole.Player,
                        EventId = 1,
                        State = Common.ParticipationState.PendingAcceptance,
                        ApplyDate = DateTime.Now.AddDays(-3).ToUniversalTime(),
                    },
                    new Participant()
                    {
                        UserId = 3,
                        Username = "Coolest_Guy",
                        //Role = Constants.EventUserRole.Player,
                        EventId = 1,
                        State = Common.ParticipationState.PendingAcceptance,
                        ApplyDate = DateTime.Now.AddHours(-10).ToUniversalTime(),
                    },
                ]
            },
            new()
            {
                Id = 2,
                CreationDate = DateTime.Now.ToUniversalTime(),
                EventCategory = Common.EventCategory.Mafia,
                EventDate = DateTime.Now.AddDays(1).ToUniversalTime(),
                EventStatus = Constants.EventStatus.Announced,
                Description = "Мафия с комиссаром и врачом",
                Title = "Мафия",
                MinParticipants = 6,
                MaxParticipants = 9,
                Location = "ул. Моховая, 7, Москва, 119019",
                //IsClosedParticipation = false,
                //MaxDuration = 4,
                OrganizerId = 1,
                Participants = [
                    new()
                    {
                        UserId = 5,
                        Username = "SheKnowsStuff",
                        //Role = Constants.EventUserRole.Player,
                        EventId = 2,
                        ApplyDate = DateTime.Now.AddDays(-4).ToUniversalTime(),
                        AcceptedDate = DateTime.Now.AddDays(-1).ToUniversalTime(),
                        State = Common.ParticipationState.Accepted
                    },
                    new()
                    {
                        UserId = 6,
                        Username = "RandomPlayer",
                        //Role = Constants.EventUserRole.Player,
                        EventId = 2,
                        ApplyDate = DateTime.Now.AddDays(-1).ToUniversalTime(),
                        State = Common.ParticipationState.PendingAcceptance
                    }
                ]
            }
        ];

        _dataContext.AddRange(events);
        _dataContext.SaveChanges();
        //var EventMember1 = ;
    }
}
