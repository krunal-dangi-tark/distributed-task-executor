using System.Collections.Concurrent;
using TaskExecutor.Models;

namespace TaskExecutor.Storage
{
    public static class StorageProvider
    {
        public static ConcurrentDictionary<string, Node> Nodes = new ConcurrentDictionary<string, Node>();
        public static ConcurrentDictionary<string, Work> Tasks = new ConcurrentDictionary<string, Work>();
        public static ConcurrentDictionary<string, NodeTask> NodeTasks = new ConcurrentDictionary<string, NodeTask>();
    }
}
