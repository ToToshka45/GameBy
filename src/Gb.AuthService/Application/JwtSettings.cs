namespace Application;

public sealed class JwtSettings
{
    public static DateTime AccessTokenExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(1);
    public static DateTime RefreshTokenExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(10);

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public bool UseSslProtection { get; set; }
}
