using RatingService.Domain.Enums;

namespace RatingService.Domain.Entities;

public class ParticipantRating : Rating
{
    public int ParticipantId { get; }
    public ParticipantRating(int participantId, Category category) : base(category)
    {
        ParticipantId = participantId;
    }

    private ParticipantRating() { }
}

