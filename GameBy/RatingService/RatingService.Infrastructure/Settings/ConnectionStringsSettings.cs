namespace RatingService.Common.Models.Settings;

public class ConnectionStringsSettings
{
    public string Npgsql { get; set; } = default!;
    public string SQLite { get; set; } = default!;

}
