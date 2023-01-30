namespace PrywatnaPrzychodniaLekarska.Authorization
{
    public class AuthResult
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}