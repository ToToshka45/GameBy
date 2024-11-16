namespace RatingService.Common.Models.Settings;

public class ConnectionStringsSettings
{
    public string NpgsqlConnectionString { get; set; } = default!;
    public string SQLiteConnectionString { get; set; } = default!;

}
