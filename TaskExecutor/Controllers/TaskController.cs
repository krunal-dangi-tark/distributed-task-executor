using Microsoft.AspNetCore.Mvc;
using TaskExecutor.BusinessServices;
using TaskExecutor.Models;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskBusinessService _taskBusinessService;

        public TaskController(TaskBusinessService taskBusinessService)
        {
            _taskBusinessService = taskBusinessService;
        }

        [HttpGet]
        public List<Work> GetAllTasks()
        {
            return _taskBusinessService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Work GetTask(string id)
        {
            return _taskBusinessService.Get(id);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterTask()
        {
            _taskBusinessService.RegisterAndAssignTask();
            return Ok();
        }

        [HttpPost]
        [Route("mark-completed")]
        public IActionResult MarkAsCompletedAsync([FromBody] string taskId)
        {
            _taskBusinessService.UpdateTaskStatus(taskId, Enum.TaskStatus.Completed);
            return Ok();
        }

        [HttpPost]
        [Route("mark-failed")]
        public IActionResult MarkAsFailed([FromBody] string taskId)
        {
            _taskBusinessService.UpdateTaskStatus(taskId, Enum.TaskStatus.Failed);
            return Ok();
        }
    }
}