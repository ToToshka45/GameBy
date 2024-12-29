using RatingService.Domain.Enums;

namespace RatingService.Application.Models.Dtos.Ratings;

public sealed class AddParticipantRatingUpdateDto(float Value, int AuthorId, DateTime CreationDate)
{
    public float Value { get; set; } = Value;
    public int AuthorId { get; set; } = AuthorId;
    public int ReceipientId { get; set; } 
    public int EventId { get; set; }
    public DateTime CreationDate { get; set; } = CreationDate;
}
