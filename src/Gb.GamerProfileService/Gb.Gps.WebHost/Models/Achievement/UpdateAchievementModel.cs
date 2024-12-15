using Domain.Entities;

namespace Gb.Gps.WebHost.Models
{
    /// <example>
    /// {
    ///    "aboutCondition": "Поучавствовать в 10 играх",
    ///    "aboutReward": "Звание Сержанта",
    ///    "rankId": "3"
    /// }
    /// </example>>
    public class UpdateAchievementModel
    {
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
    }
}
