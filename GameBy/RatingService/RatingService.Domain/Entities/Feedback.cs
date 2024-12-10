using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;

namespace RatingService.Domain.Entities;

public class Feedback : Entity<int>
{
    public int ExternalEventId { get; }
    public int AuthorId { get; }
    public Receiver Receiver { get; }
    public FeedbackContent Content { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime UpdateDate { get; }

    public Feedback(
        int eventId,
        int authorId,
        Receiver receiverInfo,
        FeedbackContent content,
        DateTime CreationDate)
    {
        ExternalEventId = eventId;
        AuthorId = authorId;
        Receiver = receiverInfo;
        Content = content;
        CreationDate = CreationDate;
    }

    private Feedback() { }

    public void SetContent(string content)
    {
        //if (String.IsNullOrEmpty(content)) return Result.Failure();
        Content = new FeedbackContent(content);
    }
}

