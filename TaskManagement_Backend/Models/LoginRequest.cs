namespace TaskManagement_Backend.Models
{
    public class LoginRequest
    {
        public string MobileNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
