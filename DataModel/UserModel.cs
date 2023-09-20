using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? RepeatPassword { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}