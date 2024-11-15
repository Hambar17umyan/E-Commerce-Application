namespace E_Commerce.API.Models.RequestModels
{
    public class LoginRequestModel
    {
        public LoginRequestModel(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}