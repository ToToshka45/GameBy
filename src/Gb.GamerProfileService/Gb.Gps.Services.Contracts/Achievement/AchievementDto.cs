using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gb.Gps.Services.Contracts
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public string AboutCondition { get; set; }
        public string AboutReward { get; set; }
        public int RankId { get; set; }
        public Rank Rank { get; set; }
        public List<GamerAchievement> GamerAchievements { get; set; } = new();
    }
}
