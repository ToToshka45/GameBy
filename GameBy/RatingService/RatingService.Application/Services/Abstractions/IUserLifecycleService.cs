using RatingService.Application.Models.Dtos.Users;

namespace RatingService.Application.Services.Abstractions;

public interface IUserLifecycleService
{
    Task<int?> AddNewUserAsync(AddUserDto newUser, CancellationToken token);
    Task<GetUserRatingsDto?> GetUserRatingsAsync(int id, CancellationToken token);
    Task<GetUserFeedbacksDto?> GetUserFeedbacksAsync(int id, CancellationToken token);
    Task<GetUserDto?> GetUserById(int id, CancellationToken token);
}
