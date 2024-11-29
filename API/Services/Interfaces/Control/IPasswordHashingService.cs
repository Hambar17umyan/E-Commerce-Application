namespace API.Services.Interfaces.Control
{
    public interface IPasswordHashingService
    {
        public string Hash(string password);
        public bool Verify(string password, string storedHash);
    }
}
