using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Aggregates;

public class UserInfo : AggregateRoot
{
    //public int ExternalUserId { get; }
    public string UserName { get; }
    
    public OrganizerRating? OrganizerRating { get; private set; }
    public GamerRating? GamerRating { get; private set; }

    private List<Feedback> _gamerFeedbacks = [];
    public IReadOnlyList<Feedback> GamerFeedbacks => _gamerFeedbacks;

    private List<Feedback> _organizerFeedbacks = [];
    public IReadOnlyList<Feedback> OrganizerFeedbacks => _organizerFeedbacks;

    public IList<Participant> Participations { get; private set; } = [];

    public UserInfo(int externalUserId, string username)
    {
        Id = externalUserId;
        //ExternalUserId = externalUserId;
        UserName = username;
        SetInitialRatings(externalUserId);
    }       

    private UserInfo() { }

    public void SetInitialRatings(int userId)
    {
        if (GamerRating is null) GamerRating = new(userId);
        if (OrganizerRating is null) OrganizerRating = new(userId);
    }

    //public void AddGamerFeedback(Feedback feedback)
    //{
    //    if (feedback.Receiver.EntityType != EntityType.Gamer)
    //        throw new InvalidDataException($"Invalid Entity Type. Expected '{EntityType.Gamer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

    //    _gamerFeedbacks.Add(feedback);
    //}

    //public void RemoveGamerFeedback(FeedbackId feedbackId)
    //{
    //    if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

    //    var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
    //    _gamerFeedbacks.Remove(feedback);
    //}

    //public void AddOrganizerFeedback(Feedback feedback)
    //{
    //    if (feedback.Receiver.EntityType != EntityType.Organizer)
    //        throw new InvalidDataException($"Invalid Entity Type.Expected '{EntityType.Organizer}', but received '{nameof(feedback.Receiver.EntityType)}'.");

    //    _organizerFeedbacks.Add(feedback);
    //}

    //public void RemoveOrganizerFeedback(FeedbackId feedbackId)
    //{
    //    if (!_gamerFeedbacks.Any(f => f.Id == feedbackId.Value)) return; // Result (NotFoundEntity)

    //    var feedback = _gamerFeedbacks.First(x => x.Id == feedbackId.Value);
    //    _gamerFeedbacks.Remove(feedback);
    //}

    //public void SetRating(Rating rating)
    //{
    //    if (TryGetRating(rating.Category, out var existingRating))
    //    {
    //        _ratings.Remove(existingRating!);
    //        _ratings.Add(rating);
    //        return;
    //    }
    //    _ratings.Add(rating);
    //}

    /// <summary>
    /// Add default ratings (0) for each <see cref="EventCategory"/> type.
    /// </summary>
    //private void InitializeRatings(int userId)
    //{
    //    EventCategory[] categories = Utilities.GetCategories();
    //    foreach (var category in categories)
    //    {
    //        SetRating(new UserRating(userId, category));
    //    }
    //}

    //private bool TryGetRating(EventCategory category, out UserRating? rating)
    //{
    //    rating = _ratings.FirstOrDefault(r => r.Category == category);
    //    return rating is null ? false : true;
    //}
}

