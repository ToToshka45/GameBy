using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class EventInfo : AggregateRoot<int>
{
    public ExternalEventId EventId { get; }
    public Category Category { get; }
    public Rating Rating { get; }

    //Feedbacks
    private List<Feedback> _feedbacks = [];
    public IReadOnlyList<Feedback> Feedbacks => _feedbacks;

    private List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants;

    // collection of RatingUpdates

    public EventInfo(int id, ExternalEventId eventId, Rating eventRating, Category category) : base(id)
    {
        EventId = eventId;
        Rating = eventRating;
        Category = category;
    }

    public void AddFeedback(Feedback feedbackInfo)
    {
        var entityType = feedbackInfo.Receiver.EntityType;
        if (entityType != EntityType.Event) throw new InvalidDataException($"Invalid entity type of the feedback: '{entityType}'.");

        _feedbacks.Add(feedbackInfo);
    }

    public void AddFeedbacks(ICollection<Feedback> feedbackInfo)
    {
        if (feedbackInfo.Any(f => f.Receiver.EntityType == EntityType.Event))
            _feedbacks.AddRange(feedbackInfo.Where(f => f.Receiver.EntityType == EntityType.Event));
    }

    public void ChangeParticipantState(ExternalParticipantId participantId, ParticipationState state)
    {
        var participant = _participants.FirstOrDefault(p => p.Id == participantId.Value);
        if (participant == null) return; // Result.Failure

        participant.SetState(state);
    }
}

