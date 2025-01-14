using RatingService.Application.Models.Dtos.Ratings;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services.Abstractions;

public interface IRatingsProcessingService
{
    Task Process(RatingUpdate update, CancellationToken token);
}
