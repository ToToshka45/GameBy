using RatingService.Domain.Exceptions;

namespace RatingService.Domain.Models.ValueObjects;

public class Description
{
    public string Content { get; }
    private Description(string content)
    {
        Content = content;
    }

    public Description Create(string content) => new Description(content);

    // TODO: think about validations for the text 
}

