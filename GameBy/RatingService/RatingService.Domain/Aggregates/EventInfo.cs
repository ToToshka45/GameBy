using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;

namespace RatingService.Domain.Aggregates;

public class EventInfo : AggregateRoot<int>
{
    public Event Event { get; }
    public EventRating EventRating { get; }

    // Feedbacks
    private List<Feedback> _feedbacks= [];
    public IReadOnlyList<Feedback> Feedbacks => _feedbacks.AsReadOnly();

    private List<ParticipantInfo> _participants = [];
    public IReadOnlyList<ParticipantInfo> Participants => _participants.AsReadOnly();


    public EventInfo(int id, Event eventEntity, EventRating eventRating, Category category) : base(id)
    {
        Event = eventEntity;
        Category = category;
        EventRating = eventRating;
    }

    public void AddFeedback(Feedback feedbackInfo) => _feedbacks.Add(feedbackInfo);
}

