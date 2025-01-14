using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RatingService.API.Models.Ratings
{
    public record EditParticipantRatingRequest
    {
        [JsonPropertyName("rating_value")]
        [Range(0.00, 5.00, ErrorMessage = "Value must be between 0.00 and 5.00")]
        public required float Value { get; set; }
    }
}
