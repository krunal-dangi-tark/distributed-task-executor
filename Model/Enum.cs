namespace TaskExecutor
{
    public class Enum
    {
        public enum NodeStatus
        {
            Available,
            Busy,
            Offline
        }
        public enum TaskStatus
        {
            Pending,
            Running,
            Completed,
            Failed
        }
    }
}
