namespace BookCatalog.Shared.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private string? Token {  get; set; }

        public void SetToken(string token)
        {
            Token = token;
        }

        public string GetToken()
        {
            return Token;
        }
    }
}
