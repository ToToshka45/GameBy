namespace RatingService.Domain.Entities.Ratings;

public abstract class IntermediateRating : RatingBase
{
    private List<RatingUpdate> _updates = [];
    public IReadOnlyList<RatingUpdate> Updates => _updates;

    protected IntermediateRating() { }

    public override void Recalculate()
    {
        var sum = _updates.Sum(u => u.Value);
        var count = _updates.Count();
        Value = sum / count;
    }

    public void AddRatingUpdate(RatingUpdate update)
    {
        if (update.RatingId is 0) update.SetRatingRelation(Id);
        _updates.Add(update);
    }
}

