namespace PrywatnaPrzychodniaLekarska.Authorization
{
    public class AuthenticationOptions
    {
        public string JwtKey { get; set; }
        public int JwtExpireIn { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtRefreshExpiresIn { get; set; }
        public string token_type { get; set; }
    }
}
