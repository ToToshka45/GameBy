using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class EventInfo : AggregateRoot<int>
{
    public string Title { get; private set; }
    public int ExternalEventId { get; }
    public DateTime CreatedAt { get; }
    public Category Category { get; set; }
    /// <summary>
    /// For recalculating rating`s value a method <see cref="Rating.Recalculate(float)"/> must be called.
    /// </summary>
    public Rating Rating { get; }

    //Feedbacks
    private List<Feedback> _feedbacks = [];
    public IReadOnlyList<Feedback> Feedbacks => _feedbacks;

    private List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants;

    public EventInfo(string title, int eventId, DateTime createdAt, Category category)
    {
        Title = title;
        ExternalEventId = eventId;
        CreatedAt = createdAt;
        Category = category;
        // set a base value for Rating
        Rating = new Rating(category);
    }

    // for EFCore
    private EventInfo() { }

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

    // TODO: decide, should we allow to change a category of event?
    public void ChangeCategory(Category category) => Category = category;
}

