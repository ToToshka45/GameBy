using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RatingService.API.Models.Ratings
{
    public record AddEventRatingUpdateRequest
    {
        [JsonPropertyName("rating_value")]
        [Range(0.00, 5.00, ErrorMessage = "Value must be between 0.00 and 5.00")]
        public required float Value { get; set; }

        [JsonPropertyName("author_id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreationDate { get; set; }
    }
}
