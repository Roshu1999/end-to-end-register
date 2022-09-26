namespace IdentityCMS.Models
{
    public interface IAuth
    {
        SecurityTokeModel GenerateToken(string username, string password);
    }
}
