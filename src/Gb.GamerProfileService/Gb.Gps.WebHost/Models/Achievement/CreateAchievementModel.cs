using Domain.Entities;

namespace Gb.Gps.WebHost.Models
{
    /// <example>
    /// {
    ///    "aboutCondition": "Провести 10 игр",
    ///    "aboutReward": "Звание Сержанта",
    ///    "rankId": "3"
    /// }
    /// </example>>
    public class CreateAchievementModel
    {
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
    }
}
