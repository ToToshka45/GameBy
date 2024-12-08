using Microsoft.Extensions.Logging;
using RatingService.Domain.Entities;
using RatingService.Domain.Enums;
using RatingService.Domain.Primitives;
using RatingService.Domain.ValueObjects.Identifiers;
using RatingService.Infrastructure.Utilities;

namespace RatingService.Domain.Aggregates;

public class UserInfo : AggregateRoot<int>
{
    public int ExternalUserId { get; }

    private List<UserRating> _ratings { get; } = [];
    public IReadOnlyCollection<UserRating> RatingsByCategory => _ratings;

    private List<Feedback> _gamerFeedbacks = [];
    public IReadOnlyList<Feedback> GamerFeedbacks => _gamerFeedbacks;

    private List<Feedback> _organizerFeedbacks = [];
    public IReadOnlyList<Feedback> OrganizerFeedbacks => _organizerFeedbacks;

    // collection of RatingUpdates

    public UserInfo(int userId)
    {
        ExternalUserId = userId;
        InitializeRatings(userId);
    }       

    private UserInfo() { }

    public void SetRating(UserRating rating)
    {
        if (TryGetRating(rating.Category, out var existingRating))
        {
            _ratings.Remove(existingRating!);
            _ratings.Add(rating);
            return;
        }
        _ratings.Add(rating);
    }
    
    public void AddGamerFeedback(Feedback feedback)
    {
        if (feedback.Receiver.EntityType != EntityType.Gamer)
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Gamer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

        _gamerFeedbacks.Add(feedback);
    }

    public void RemoveGamerFeedback(FeedbackId feedbackId)
    {
        if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

        var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
        _gamerFeedbacks.Remove(feedback);
    }

    public void AddOrganizerFeedback(Feedback feedback)
    {
        if (feedback.Receiver.EntityType != EntityType.Organizer)
            throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Organizer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

        _organizerFeedbacks.Add(feedback);
    }

    public void RemoveOrganizerFeedback(FeedbackId feedbackId)
    {
        if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

        var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
        _gamerFeedbacks.Remove(feedback);
    }
    
    /// <summary>
    /// Add default ratings (0) for each <see cref="Category"/> type.
    /// </summary>
    private void InitializeRatings(int userId)
    {
        Category[] categories = Utilities.GetCategories();
        foreach (var category in categories)
        {
            SetRating(new UserRating(userId, category));
        }
    }

    private bool TryGetRating(Category category, out UserRating? rating)
    {
        rating = _ratings.FirstOrDefault(r => r.Category == category);
        return rating is null ? false : true;
    }
}

