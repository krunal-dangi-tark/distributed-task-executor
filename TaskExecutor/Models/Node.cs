namespace TaskExecutor.Models
{
    public class Node
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Enum.NodeStatus Status { get; set; } = Enum.NodeStatus.Available;
    }
}
