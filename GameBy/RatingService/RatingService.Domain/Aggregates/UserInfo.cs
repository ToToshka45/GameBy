using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class UserInfo : AggregateRoot<int>
{
    public UserId UserId { get; }
    public UserRating RatingInfo { get; }

    // Feedbacks
    private List<Feedback> _gamerFeedbacks = [];
    public IReadOnlyList<Feedback> GamerFeedbacks => _gamerFeedbacks;

    private List<Feedback> _organizerFeedbacks = [];
    public IReadOnlyList<Feedback> OrganizerFeedbacks => _organizerFeedbacks;

    public UserInfo(int id, UserId userId, UserRating ratingInfo) : base(id)
    {
        UserId = userId;
        RatingInfo = ratingInfo;
    }

    public void AddGamerFeedback(Feedback feedback)
    {
        if (feedback.Receiver.EntityType != EntityType.Gamer)
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Gamer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

        _gamerFeedbacks.Add(feedback);
    }

    public void RemoveGamerFeedback(FeedbackId feedbackId)
    {
        if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

        var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
        _gamerFeedbacks.Remove(feedback);
    }

    public void AddOrganizerFeedback(Feedback feedback)
    {
        if (feedback.Receiver.EntityType != EntityType.Organizer)
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Organizer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

        _organizerFeedbacks.Add(feedback);
    }

    public void RemoveOrganizerFeedback(FeedbackId feedbackId)
    {
        if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

        var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
        _gamerFeedbacks.Remove(feedback);
    }
}

