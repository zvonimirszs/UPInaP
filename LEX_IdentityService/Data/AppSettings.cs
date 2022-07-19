namespace LEX_IdentityService.Data;
public class AppSettings
{
    public string Secret { get; set; }

    public int RefreshTokenTTL { get; set; }
}