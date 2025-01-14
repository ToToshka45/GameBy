using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Enums;
using RatingService.Domain.Exceptions;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class EventInfo : AggregateRoot
{
    //public int ExternalEventId { get; }
    public string Title { get; private set; }
    public int OrganizerId { get; }
    public DateTime CreationDate { get; }
    public EventCategory Category { get; private set; }
    public EventProgressionState State { get; private set; }

    public EventRating? Rating { get; private set; }

    //Feedbacks
    private List<Feedback> _feedbacks = [];
    public IReadOnlyList<Feedback> Feedbacks => _feedbacks;

    private List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants;

    public EventInfo(int externalEventId, string title, int organizerId, 
        DateTime creationDate, EventCategory category, EventProgressionState state)
    {
        Id = externalEventId;
        OrganizerId = organizerId;
        //ExternalEventId = externalEventId;
        Title = title;
        CreationDate = creationDate;
        Category = category;
        State = state;

        SetInitialRatings(externalEventId, organizerId);
    }

    private EventInfo() { }

    public void SetInitialRatings(int externalEventId, int organizerId)
    {
        if (Rating is null) Rating = new(externalEventId, organizerId);
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

    public void AddParticipant(Participant participant) => _participants.Add(participant);

    // TODO: decide, should we allow to change a category of event?
    public void ChangeCategory(EventCategory category) => Category = category;

    public void ValidateParticipant(int externalParticipantId)
    {
        if (_participants.Any(p => p.Id == externalParticipantId))
            throw new ParticipantExistsException(externalParticipantId);
    }
}