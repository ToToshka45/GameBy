using RatingService.Domain.Models.ValueObjects;

namespace RatingService.Domain.Models.Entities;

public class ParticipantInfo
{
    public int Id { get; private set; }
    public ParticipantId ParticipantId { get; }
    public UserId UserId { get; }
    public Rating Rating { get; }
    private ParticipantInfo(UserId userId, Rating rating) { UserId = userId; Rating = rating; }

    public static ParticipantInfo Create(UserId id, Rating rating) => new ParticipantInfo(id, rating);
}
