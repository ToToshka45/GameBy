using RatingService.Application.Models.Dtos.Users;

namespace RatingService.Application.Services.Abstractions;

public interface IUserLifecycleService
{
    Task<GetUserInfoDto?> AddNewUserAsync(AddUserDto newUser, CancellationToken token);
    //Task<GetUserRatingsDto?> GetUserRatingsAsync(int id, CancellationToken token);
    Task<GetUserFeedbacksDto?> GetUserFeedbacksAsync(int id, CancellationToken token);
    Task<IEnumerable<GetUserInfoDto>> GetUsersInfo(CancellationToken token);
    Task<GetUserInfoDto?> GetUserInfoById(int id, CancellationToken token);
}
