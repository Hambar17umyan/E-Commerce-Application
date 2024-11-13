namespace E_Commerce.API.Services
{
    public class PasswordHashingService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        }
    }
}
