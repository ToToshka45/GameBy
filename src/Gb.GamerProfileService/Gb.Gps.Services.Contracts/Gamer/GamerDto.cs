using Domain.Entities;
using Gb.Gps.Services.Contracts;

namespace Services.Contracts.Gamer
{
    public class GamerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        //public DateOnly DateOfBirth { get; set; }
        public string AboutMe { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ContactMe { get; set; } = null!;
        public int RankId { get; set; }
        public RankDto? Rank { get; set; }
    }
}
