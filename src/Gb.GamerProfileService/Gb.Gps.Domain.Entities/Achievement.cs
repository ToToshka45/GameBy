﻿namespace Domain.Entities
{
    public class Achievement : BaseEntity
    {
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
        public Rank Rank { get; set; }
        public List<GamerAchievement> GamerAchievements { get; set; } = new();
    }
}
