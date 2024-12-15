using Domain.Entities;

namespace Gb.Gps.WebHost.Models
{
    public class AchievementModel
    {
        public int Id { get; set; }
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
        public RankModel Rank { get; set; }
    }
}
