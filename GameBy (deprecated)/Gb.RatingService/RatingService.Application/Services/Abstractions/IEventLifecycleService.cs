using RatingService.Application.Models.Dtos.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Application.Models.Dtos.Ratings;

namespace RatingService.Application.Services.Abstractions;

public interface IEventLifecycleService
{
    Task<GetEventInfoDto?> AddNewEventAsync(CreateEventDto dto, CancellationToken token);
    Task<ICollection<GetEventInfoDto>> GetEventsAsync(CancellationToken token);
    Task<GetEventInfoDto?> GetEventByIdAsync(int id, CancellationToken token);
    Task<GetParticipantDto?> AddParticipantAsync(int eventId, AddParticipantDto dto, CancellationToken token);
    Task RemoveParticipantByEventIdAsync(int eventId, int participantId, CancellationToken token);
    Task<GetParticipantDto?> GetParticipantByEventIdAsync(int eventId, int participantId, CancellationToken token);
    Task<IEnumerable<GetParticipantDto>> GetParticipantsByEventIdAsync(int eventId, CancellationToken token);
    Task FinalizeEventAsync(FinalizeEventDto dto, CancellationToken token);
    Task AddParticipantRatingUpdateAsync(AddParticipantRatingUpdateDto dto, CancellationToken token);
    Task AddEventRatingUpdateAsync(AddEventRatingUpdateDto dto, CancellationToken token);
    //Task EditParticipantRating(int ratingUpdateId, EditParticipantRatingDto dto, CancellationToken token);
}
