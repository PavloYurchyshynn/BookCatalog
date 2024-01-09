namespace BookCatalog.Shared.Services
{
    public interface IJwtTokenService
    {
        void SetToken(string token);
        string GetToken();
    }
}
