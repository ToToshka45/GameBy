using RatingService.Domain.Abstractions;
using RatingService.Domain.Models.ValueObjects;
using RatingService.Domain.Models.ValueObjects.Identifiers;

namespace RatingService.Domain.Models.Entities;

public class EventInfo : BaseEntity
{
    public EventId EventId { get; }
    public Rating Rating { get; set; }

    // Feedbacks
    private List<FeedbackInfo> _feedbacksInfo;
    public IReadOnlyList<FeedbackInfo> FeedbacksInfo => _feedbacksInfo;
    private EventInfo(EventId eventId, Rating rating) { 
        EventId = eventId; 
        Rating = rating; 
        _feedbacksInfo = new List<FeedbackInfo>();
    }

    public static EventInfo Create(EventId eventId, Rating rating) => new EventInfo(eventId, rating);
    public void AddFeedback(FeedbackInfo feedbackInfo) => _feedbacksInfo.Add(feedbackInfo);
}

