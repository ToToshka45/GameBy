using RatingService.Domain.Models.Primitives;

namespace RatingService.Domain.Models.ValueObjects;

public class FeedbackDetails : ValueObject
{
    public string Content { get; }
    private FeedbackDetails(string content)
    {
        Content = content;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        return [Content];
    }
}

