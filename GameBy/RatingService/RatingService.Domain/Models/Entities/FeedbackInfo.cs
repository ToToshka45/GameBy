using RatingService.Domain.Models.Primitives;
using RatingService.Domain.Models.ValueObjects;
using RatingService.Domain.Models.ValueObjects.Identifiers;

namespace RatingService.Domain.Models.Entities;

public class FeedbackInfo : Entity
{
    public EventId EventId { get; }
    public AuthorId AuthorId { get; }
    public ReceiverDetails ReceiverDetails { get; }
    public FeedbackDetails Content { get; }
    public DateTime CreationDate { get; }
    public DateTime UpdateDate { get; private set; }
    private FeedbackInfo(EventId eventId, AuthorId authorId, ReceiverDetails receiverInfo, FeedbackDetails content, DateTime createdAt)
    {
        EventId = eventId;
        AuthorId = authorId;
        ReceiverDetails = receiverInfo;
        Content = content;
        CreationDate = createdAt;
    }

    public static FeedbackInfo Create(EventId eventId, AuthorId authorId, ReceiverDetails receiverInfo, FeedbackDetails content, DateTime createdAt) => 
        new FeedbackInfo(eventId, authorId, receiverInfo, content, createdAt);
}

