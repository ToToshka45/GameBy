using Common;

namespace WebApi.Dto
{
    public class ShortEventResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PlayersLimit { get; set; }

        public int CurrentPlayersCount { get; set; }

        public EventCategory Category { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }
    }
}
