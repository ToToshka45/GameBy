namespace Domain.Entities
{
    public class GamerAchievement
    {
        public int GamerId { get; set; }
        public Gamer Gamer { get; set; }

        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
