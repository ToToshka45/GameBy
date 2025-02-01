using Microsoft.Extensions.Logging;
using RatingService.Application.Configurations.Mappings;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Application.Services.Abstractions;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;

namespace RatingService.Application.Services.LifecycleServices;

public class UserLifecycleService : IUserLifecycleService
{
    private readonly IRepository<UserInfo> _userRepo;
    private readonly ILogger<EventLifecycleService> _logger;

    public UserLifecycleService(ILogger<EventLifecycleService> logger, IRepository<UserInfo> userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    public async Task<GetUserInfoDto?> AddNewUserAsync(AddUserDto newUser, CancellationToken token)
    {
        try
        {
            _logger.LogInformation($"Adding a new User with External Id '{newUser.ExternalUserId}'");
            var userInfo = newUser.ToUserInfo();
            var savedUser = await _userRepo.Add(userInfo, token);
            if (savedUser == null) { return null; }
            //savedUser.SetInitialRatings();
            //await _userRepo.SaveChangesAsync(token);
            if (savedUser == null) { return null; }

            return savedUser.ToGetUserInfoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occured while adding a new User instance.");
            throw;
        }
    }
    public async Task<IEnumerable<GetUserInfoDto>> GetUsersInfo(CancellationToken token)
    {
        var users = await _userRepo.GetAll(token);
        return users.ToGetUserInfoDtoList();
    }

    public async Task<GetUserInfoDto?> GetUserInfoById(int id, CancellationToken token)
    {
        var user = await _userRepo.GetById(id, token);
        if (user == null) { return null; }
        return user.ToGetUserInfoDto();
    }

    //public async Task<GetUserRatingsDto?> GetUserRatingsAsync(int id, CancellationToken token)
    //{
    //    var user = await _userRepo.GetEntityWithIncludesAsync(id, token, 
    //        [e => e!.GamerRating, e => e!.OrganizerRating]);
    //    if (user == null) { return null; }
    //    return user.ToGetUserRatingsDto();
    //}

    public async Task<GetUserFeedbacksDto?> GetUserFeedbacksAsync(int id, CancellationToken token)
    {
        var user = await _userRepo.GetEntityWithIncludesAsync(id, token, [e => e.GamerFeedbacks, e => e.OrganizerFeedbacks]);
        if (user == null) { return null; }
        return user.ToGetUserFeedbacksDto();
    }

}
