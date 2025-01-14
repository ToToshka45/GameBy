using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;

namespace RatingService.Domain.Entities;

public class Feedback : Entity<int>
{
    public int ExternalEventId { get; }
    public int AuthorId { get; }
    public Receiver Receiver { get; }
    public string Content { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime UpdateDate { get; }

    public Feedback(
        int externalEventId,
        int authorId,
        Receiver receiverInfo,
        string content,
        DateTime creationDate)
    {
        ExternalEventId = externalEventId;
        AuthorId = authorId;
        Receiver = receiverInfo;
        Content = content;
        CreationDate = CreationDate;
    }

    private Feedback() { }

    public void SetContent(string content)
    {
        //if (String.IsNullOrEmpty(content)) return Result.Failure();
        Content = content;
    }
}

