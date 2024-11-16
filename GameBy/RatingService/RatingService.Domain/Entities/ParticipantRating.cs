using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects;
using RatingService.Domain.ValueObjects.Identifiers;

namespace RatingService.Domain.Aggregates;

public class ParticipantRating : Entity<int>
{
    public UserId UserId { get; }
    public ParticipantId ParticipantId { get; }
    public Rating RatingValue { get; }
    public Category Category { get; }

    public ParticipantRating(
        int id,
        UserId userId,
        Rating value,
        ParticipantId participantId,
        Category category) : base(id)
    {
        UserId = userId;
        RatingValue = value;
        ParticipantId = participantId;
        Category = category;
    }
}

