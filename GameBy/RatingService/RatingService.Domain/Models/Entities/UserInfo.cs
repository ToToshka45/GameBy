using RatingService.Domain.Enums;
using RatingService.Domain.Models.ValueObjects;
using RatingService.Domain.Models.ValueObjects.Identifiers;

namespace RatingService.Domain.Models.Entities;

public class UserInfo
{
    public int Id { get; private set; }
    public UserId UserId { get; }
    public Rating Rating { get; }

    // Feedbacks
    private List<FeedbackInfo> _participantFeedbacksInfo;
    public IReadOnlyList<FeedbackInfo> ParticipantFeedbacksInfo => _participantFeedbacksInfo;

    private List<FeedbackInfo> _organizerFeedbacksInfo;
    public IReadOnlyList<FeedbackInfo> OrganizerFeedbacksInfo => _organizerFeedbacksInfo;

    private UserInfo(UserId userId, Rating rating)
    {
        UserId = userId;
        Rating = rating;
        _participantFeedbacksInfo = new List<FeedbackInfo>();
        _organizerFeedbacksInfo = new List<FeedbackInfo>();
    }
    public static UserInfo Create(UserId id, Rating rating) => new UserInfo(id, rating);

    public void AddParticipantFeedback(FeedbackInfo feedback)
    {
        if (feedback.ReceiverInfo.EntityType != EntityType.Participant) 
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Participant}', but received '{nameof(feedback.ReceiverInfo.EntityType)}'.");

        _participantFeedbacksInfo.Add(feedback);
    }
    public void AddOrganizerFeedback(FeedbackInfo feedback)
    {
        if (feedback.ReceiverInfo.EntityType != EntityType.Participant)
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Participant}', but received '{nameof(feedback.ReceiverInfo.EntityType)}'.");

        _participantFeedbacksInfo.Add(feedback);
    }
}

