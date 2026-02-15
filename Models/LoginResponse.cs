namespace PosPizza.Models
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public UserData Usuario { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string nombreUsuario { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
