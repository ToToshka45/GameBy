using RatingService.Domain.Abstractions;
using RatingService.Domain.Models.ValueObjects;
using RatingService.Domain.Models.ValueObjects.Identifiers;

namespace RatingService.Domain.Models.Entities;

public class FeedbackInfo : BaseEntity
{
    public EventId EventId { get; }
    public AuthorId AuthorId { get; }
    public ReceiverInfo ReceiverInfo { get; }
    public FeedbackContent Content { get; }
    private FeedbackInfo(EventId eventId, AuthorId authorId, ReceiverInfo receiverInfo, FeedbackContent content)
    {
        EventId = eventId;
        AuthorId = authorId;
        ReceiverInfo = receiverInfo;
        Content = content;
    }

    public static FeedbackInfo Create(EventId eventId, AuthorId authorId, ReceiverInfo receiverInfo, FeedbackContent content) => 
        new FeedbackInfo(eventId, authorId, receiverInfo, content);
}

