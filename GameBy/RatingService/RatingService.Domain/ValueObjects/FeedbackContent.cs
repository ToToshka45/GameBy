using RatingService.Domain.Primitives;

namespace RatingService.Domain.ValueObjects;

public class FeedbackContent : ValueObject
{
    public string Content { get; }
    public FeedbackContent(string content)
    {
        Content = content;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Content;
    }
}

