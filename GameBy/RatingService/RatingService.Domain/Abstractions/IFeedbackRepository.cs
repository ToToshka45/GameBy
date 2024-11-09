using RatingService.Domain.Models.Entities;
using System.Linq.Expressions;

namespace RatingService.Domain.Abstractions;

public interface IFeedbackRepository<TFeedbackInfo> : IRepository<TFeedbackInfo> where TFeedbackInfo : FeedbackInfo
{
    Task<IEnumerable<TFeedbackInfo>> GetByFilterAsync(Expression filter);
}
