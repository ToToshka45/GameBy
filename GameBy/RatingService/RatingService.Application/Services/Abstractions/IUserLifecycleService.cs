using RatingService.Application.Models.Dtos.Users;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Services.Abstractions;

public interface IUserLifecycleService
{
    Task<GetUserInfoDto?> AddNewUserAsync(AddUserDto newUser, CancellationToken token);
    //Task<GetUserRatingsDto?> GetUserRatingsAsync(int id, CancellationToken token);
    Task<GetUserFeedbacksDto?> GetUserFeedbacksAsync(int id, CancellationToken token);
    Task<GetUserInfoDto?> GetUserById(int id, CancellationToken token);
}
