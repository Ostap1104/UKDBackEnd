namespace ITSchool.Core.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
