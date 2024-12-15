using Domain.Entities;

namespace Gb.Gps.WebHost.Models
{
    public class CreateAchievementModel
    {
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
        public Rank Rank { get; set; }
        public List<GamerAchievement> GamerAchievements { get; set; } = new();
    }
}
