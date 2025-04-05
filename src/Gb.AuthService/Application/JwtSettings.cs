namespace Application;

public sealed class JwtSettings
{
    public static int AccessTokenExpiresInMinutes { get; set; } = 1;
    public static int RefreshTokenExpiresInMinutes { get; set; } = 10;

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public bool UseSslProtection { get; set; }
}
