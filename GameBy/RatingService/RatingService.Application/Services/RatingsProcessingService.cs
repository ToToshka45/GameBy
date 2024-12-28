using Microsoft.Extensions.Logging;
using RatingService.Application.Services.Abstractions;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;

internal class RatingsProcessingService : IRatingsProcessingService
{
    private readonly ILogger<RatingsProcessingService> _logger;
    private readonly IRepository<EventInfo> _eventRepo;
    private readonly IRepository<UserInfo> _userRepo;
    private readonly IRatingsRepository _ratingsRepo;

    public RatingsProcessingService(
        ILogger<RatingsProcessingService> logger,
        IRatingsRepository ratingsRepo,
        IRepository<EventInfo> eventRepo,
        IRepository<UserInfo> userRepo)
    {
        _logger = logger;
        _ratingsRepo = ratingsRepo;
        _eventRepo = eventRepo;
        _userRepo = userRepo;
    }

    public async Task Process(RatingUpdate update, CancellationToken token)
    {
        _logger.LogInformation($"Starting processing the entity of type '{update.EntityType}' with Id '{update.SubjectId}'.");
        await _ratingsRepo.AddOrUpdate(update, token);
    }
}
