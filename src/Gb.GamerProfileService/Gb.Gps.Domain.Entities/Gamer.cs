using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Gamer : BaseEntity
{
    public required string Name { get; set; }
    public required string Nickname { get; set; }

    [DataType( DataType.Date )]
    [Column( TypeName = "Date" )]
    public DateTime DateOfBirth { get; set; }
    public string? AboutMe { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? ContactMe { get; set; }
    //public string PhotoPath? { get; set; }
    public virtual List<GamerAchievement> GamerAchievements { get; set; } = new();
    public int? RankId { get; set; }
    public virtual Rank? Rank { get; set; }
}
