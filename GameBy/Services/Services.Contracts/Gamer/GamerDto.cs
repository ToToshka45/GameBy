namespace Services.Contracts.Gamer
{
    public class GamerDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }
        //public DateOnly DateOfBirth { get; set; }
        public string? AboutMe { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? ContactMe { get; set; }
    }
}
