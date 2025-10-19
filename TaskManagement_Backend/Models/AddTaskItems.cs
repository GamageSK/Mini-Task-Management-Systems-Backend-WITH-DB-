namespace TaskManagement_Backend.Models
{
    public class AddTaskItems
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "To Do";
        public DateTime DueDate { get; set; }


    }
}
