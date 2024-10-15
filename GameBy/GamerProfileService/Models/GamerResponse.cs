namespace GamerProfileService.Models;

public class GamerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Nickname { get; set; }
    //public DateOnly DateOfBirth { get; set; }
    public string AboutMe { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string ContactMe { get; set; }
}
