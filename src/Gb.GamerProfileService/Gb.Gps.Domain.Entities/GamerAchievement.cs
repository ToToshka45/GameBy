namespace Domain.Entities
{
    public class GamerAchievement
    {
        public int GamerId { get; set; }
        public virtual Gamer Gamer { get; set; }

        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}
