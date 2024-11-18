using System.Security.Cryptography;

namespace API.Services.Control
{
    public class PasswordHashingService
    {
        public string Hash(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = HashPasswordWithSalt(password, salt);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }
        public bool Verify(string password, string storedHash)
        {
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Invalid stored hash format.");
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);
            byte[] actualHash = HashPasswordWithSalt(password, salt);

            return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
        }

        private byte[] GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] HashPasswordWithSalt(string password, byte[] salt, int iterations = 100, int hashLength = 32)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(hashLength);
            }
        }
    }
}
