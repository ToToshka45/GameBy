using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class EventInfo : AggregateRoot<int>
{
    public Event Event { get; }
    public EventRating EventRating { get; }

    // Feedbacks
    private List<Feedback> _feedbacks = [];
    public IReadOnlyList<Feedback> Feedbacks => _feedbacks.AsReadOnly();

    private List<ParticipantInfo> _participants = [];
    public IReadOnlyList<ParticipantInfo> Participants => _participants.AsReadOnly();


    public EventInfo(int id, Event eventEntity, EventRating eventRating) : base(id)
    {
        Event = eventEntity;
        EventRating = eventRating;
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

    public void ChangeParticipantState(ParticipantId participantId, ParticipationState state)
    {
        var participant = _participants.FirstOrDefault(p => p.Id == participantId.Value);
        if (participant == null) return; // Result.Failure

        participant.SetState(state);
    }
}

