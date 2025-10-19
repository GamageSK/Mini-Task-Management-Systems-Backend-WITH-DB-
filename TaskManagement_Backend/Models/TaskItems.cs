namespace TaskManagement_Backend.Models
{
    public class TaskItems
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "To Do";
        public DateTime DueDate { get; set; }
        public string FormattedDueDate => DueDate.ToString("yyyy-MM-dd");

    }
}
