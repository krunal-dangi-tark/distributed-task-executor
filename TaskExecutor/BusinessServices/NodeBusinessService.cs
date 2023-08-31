using System.Xml.Linq;
using TaskExecutor.Models;
using TaskExecutor.Storage;

namespace TaskExecutor.BusinessServices
{
    public class NodeBusinessService
    {
        public List<Node> GetAllNodes()
        {
            return StorageProvider.Nodes.Select(x => x.Value).ToList();
        }

        public void RegisterNode(NodeRegistrationRequest node)
        {
            Node newNode = new Node()
            {
                Name = node.Name,
                Address = node.Address
            };
            StorageProvider.Nodes.TryAdd(node.Name, newNode);
            // _ = AssignTaskAsync();
            //Task.Run(async () =>
            //{
            //    await Task.Delay(1000);
            //    await AssignTaskAsync();
            //});
        }     


        public void UnregisterNode(string name)
        {
            Node node = StorageProvider.Nodes.FirstOrDefault(x => x.Value.Name == name).Value;
            if (node != null)
            {
                if (node.Status.ToString() == Enum.NodeStatus.Busy.ToString())
                {
                    StorageProvider.NodeTasks.TryRemove(node.Name, out NodeTask assignedTask);
                    if (assignedTask != null)
                    {
                        assignedTask.Work.Status = Enum.TaskStatus.Pending;
                        assignedTask.Node.Status = Enum.NodeStatus.Offline;
                        StorageProvider.Nodes.TryRemove(node.Name, out var removedNode);
                    }
                }
            }
            else
            {
                throw new Exception("Node not found");
            }
        }       
    }
}