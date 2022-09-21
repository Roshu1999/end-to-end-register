namespace IdentityCMS.Models
{
    public class IAuthService
    {
        SecurityTokenModel GenerateToken(string username, string password, int roleId = 0);
    }
}
