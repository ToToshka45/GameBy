using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RatingService.API.Models.Ratings
{
    public record AddParticipantRatingUpdateRequest
    {
        [JsonPropertyName("rating_value")]
        [Range(0.00, 5.00, ErrorMessage = "Value must be between 0.00 and 5.00")]
        public required float Value { get; set; }
        [JsonPropertyName("entity_type")]
        public required string EntityType { get; set; }
        [JsonPropertyName("author_id")]
        public int AuthorId { get; set; }
        [JsonPropertyName("participant_id")]
        public required int SubjectId { get; set; }
        
        [JsonPropertyName("event_id")]
        public int EventId { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreationDate { get; set; }
    }
}
