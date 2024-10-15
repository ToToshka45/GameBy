namespace GameBy.Core.Domain.Entities;

public class Gamer : BaseEntity
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string AboutMe { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string ContactMe { get; set; }
    //public string PhotoPath { get; set; }
    //public string Status { get; set; }
}
