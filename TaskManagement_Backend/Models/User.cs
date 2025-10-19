namespace TaskManagement_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
