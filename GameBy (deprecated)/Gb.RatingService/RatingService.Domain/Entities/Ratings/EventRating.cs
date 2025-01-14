namespace RatingService.Domain.Entities.Ratings;

public class EventRating : RatingBase
{
    public int EventInfoId { get; }
    public int OrganizerRatingId { get; }
    //public int ExternalEventId { get; }

    private List<EventRatingUpdate> _updates = [];
    public IReadOnlyList<EventRatingUpdate> Updates => _updates;

    public EventRating(int eventInfoId, int organizerRatingId)
    {
        Id = eventInfoId;
        EventInfoId = eventInfoId;
        OrganizerRatingId = organizerRatingId;
        //ExternalEventId = externalEventId;
    }

    private EventRating() { }

    public void AddRatingUpdate(EventRatingUpdate update)
    {
        if (update.RatingId is 0) update.SetRatingRelation(Id);
        _updates.Add(update);
    }
}

