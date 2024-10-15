using System.ComponentModel.DataAnnotations;

namespace GamerProfileService.Models;

public class CreateOrEditGamerRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Nickname { get; set; }

    //public DateOnly DateOfBirth { get; set; }

    public string AboutMe { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string ContactMe { get; set; }
}
