using RatingService.Domain.Abstractions;
using RatingService.Domain.Models.Entities;
using System.Linq.Expressions;

namespace RatingService.Application.Repositories;

public class FeedbackRepository : IFeedbackRepository<FeedbackInfo>
{
    public Task Add(FeedbackInfo entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Get(FeedbackInfo entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeedbackInfo>> GetByFilterAsync(Expression filter)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, FeedbackInfo entity)
    {
        throw new NotImplementedException();
    }
}
