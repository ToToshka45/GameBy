namespace RatingService.Application.Models.Dtos.Ratings;

public sealed class AddEventRatingUpdateDto(float Value, int AuthorId, DateTime CreationDate)
{
    public float Value { get; set; } = Value;
    public int AuthorId { get; set; } = AuthorId;
    public int EventId { get; set; }
    public DateTime CreationDate { get; set; } = CreationDate;
}
