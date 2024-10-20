namespace GameBy.Core.Domain.Entities;

public class Gamer : BaseEntity
{
    public required string Name { get; set; }
    public required string Nickname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? AboutMe { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? ContactMe { get; set; }
    //public string PhotoPath? { get; set; }
    //public string Status { get; set; }
}
