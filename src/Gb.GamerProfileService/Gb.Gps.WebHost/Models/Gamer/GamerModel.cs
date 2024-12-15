using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;

namespace GamerProfileService.Models
{
    public class GamerModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? AboutMe { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? ContactMe { get; set; }
        public int RankId { get; set; }
        public RankModel? Rank { get; set; }
    }
}
