using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class Feedback : AggregateRoot<int>
{
    public Event Event { get; }
    public AuthorId AuthorId { get; }
    public Receiver Receiver { get; }
    public FeedbackContent Content { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime UpdateDate { get; }

    public Feedback(
        int id,
        Event eventId,
        AuthorId authorId,
        Receiver receiverInfo,
        string content,
        DateTime createdAt) : base(id)
    {
        Event = eventId;
        AuthorId = authorId;
        Receiver = receiverInfo;
        Content = new FeedbackContent(content);
        CreationDate = createdAt;
    }

    public void SetContent(string content)
    {
        //if (String.IsNullOrEmpty(content)) return Result.Failure();
        Content = new FeedbackContent(content);
    }
}

