namespace GamerProfileService.Models.Gamer
{
    public class CreateGamerModel
    {
        public string Name { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        //public DateOnly DateOfBirth { get; set; }
        public string? AboutMe { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? ContactMe { get; set; }
    }
}
