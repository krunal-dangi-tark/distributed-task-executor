namespace TaskExecutor.Models
{
    public class Work
    {
        public Guid Id { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public DateTimeOffset? TimeStamp { get; set; } = DateTimeOffset.UtcNow;
    }
}
