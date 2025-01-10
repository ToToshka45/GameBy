namespace GamerProfileService.Models
{
    public class UpdateGamerModel
    {
        public string Name { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ContactMe { get; set; } = null!;
        public int RankId { get; set; }
    }
}
