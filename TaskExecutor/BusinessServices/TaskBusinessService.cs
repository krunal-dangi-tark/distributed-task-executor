using TaskExecutor.Models;
using TaskExecutor.Storage;

namespace TaskExecutor.BusinessServices
{
    public class TaskBusinessService
    {
        private readonly NodeBusinessService _nodeBusinessService;

        public TaskBusinessService(NodeBusinessService nodeBusinessService)
        {
            _nodeBusinessService = nodeBusinessService;
        }

        public List<Work> GetAll()
        {
            return StorageProvider.Tasks.Select(x => x.Value).OrderBy(x => x.DateAdded).ToList();
        }

        public Work Get(string workId)
        {
            StorageProvider.Tasks.TryGetValue(workId, out Work work);
            return work; 
        }

        public void RegisterAndAssignTask()
        {
            Work work = new Work() { 
                Id = Guid.NewGuid(),
                Status = Enum.TaskStatus.Pending
            };
            StorageProvider.Tasks.TryAdd(work.Id.ToString(), work);
            // _ = _nodeBusinessService.AssignTaskAsync();
        }

        public void UpdateTaskStatus(string taskId, Enum.TaskStatus taskStatus)
        {
            StorageProvider.NodeTasks.TryRemove(taskId, out NodeTask nodeTask);
            if (nodeTask != null)
            {
                nodeTask.Work.Status = taskStatus;
                nodeTask.Node.Status = Enum.NodeStatus.Available;
               //  _ = _nodeBusinessService.AssignTaskAsync();
            }
        }
    }
}
