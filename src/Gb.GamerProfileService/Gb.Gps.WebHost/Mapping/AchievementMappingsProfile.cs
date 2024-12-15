using AutoMapper;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;

namespace AchievementProfileService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности игрока.
    /// </summary>
    public class AchievementMappingsProfile : Profile
    {
        public AchievementMappingsProfile()
        {
            CreateMap<AchievementDto, AchievementModel>();
            CreateMap<CreateAchievementModel, CreateAchievementDto>();
            CreateMap<UpdateAchievementModel, UpdateAchievementDto>();
        }
    }
}
