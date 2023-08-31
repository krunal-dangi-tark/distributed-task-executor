namespace TaskExecutor.Models
{
    public class Work
    {
        public Guid Id { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public DateTime? DateAdded { get; set; } = DateTime.UtcNow;
        public DateTime? DateEdited { get; set; }
    }
}
