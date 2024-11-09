using RatingService.Domain.Exceptions;

namespace RatingService.Domain.Models.ValueObjects;

public class FeedbackContent
{
    public string Content { get; }
    private FeedbackContent(string content)
    {
        Content = content;
    }
}

