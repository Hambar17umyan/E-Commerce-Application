namespace API.Models.Request
{
    public class LoginRequestModel
    {
        public LoginRequestModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
