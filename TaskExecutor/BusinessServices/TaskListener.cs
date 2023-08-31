using TaskExecutor.Models;
using TaskExecutor.Storage;

namespace TaskExecutor.BusinessServices
{
    public class TaskListener
    {
        private readonly TaskBusinessService _taskBusinessService;
        public TaskListener(WebApplicationBuilder builder)
        {
            var provider = builder.Services.BuildServiceProvider();
            _taskBusinessService = provider.GetRequiredService<TaskBusinessService>();
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await AssignTaskAsync();
                        // await TaskTimeChecker();
                    }
                    catch (Exception ex) { }
                }
            });
        }

        private async Task TaskTimeChecker()
        {
            // TODO: Mark as failed 
        }

        private async Task AssignTaskAsync()
        {
            Work work = StorageProvider.Tasks.OrderBy(x => x.Value.DateAdded).FirstOrDefault(x => x.Value.Status.ToString() == Enum.TaskStatus.Pending.ToString()).Value;
            Node node = StorageProvider.Nodes.FirstOrDefault(x => x.Value.Status.ToString() == Enum.NodeStatus.Available.ToString()).Value;
            if (work != null && node != null)
            {

                NodeTask nodeTask = new NodeTask()
                {
                    Node = node,
                    Work = work
                };
                var response = await AssignTaskToNodeAsync(nodeTask);
                if (response.IsSuccessStatusCode)
                {
                    work.Status = Enum.TaskStatus.Running;
                    node.Status = Enum.NodeStatus.Busy;
                    StorageProvider.NodeTasks.TryAdd(work.Id.ToString(), nodeTask);
                }
            }
        }

        public async Task<HttpResponseMessage> AssignTaskToNodeAsync(NodeTask nodeTask)
        {
            var client = new HttpClient();
            return await client.PostAsJsonAsync($"{nodeTask.Node.Address}/api/Worker/assign-task", nodeTask.Work);  
        }
    }
}
