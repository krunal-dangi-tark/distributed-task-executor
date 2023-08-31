using Microsoft.AspNetCore.Mvc;
using TaskExecutor.Models;
using Worker.BusinessServices;

namespace Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerBusinessService _workerBusinessService;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(WorkerBusinessService workerBusinessService, ILogger<WorkerController> logger)
        {
            _workerBusinessService = workerBusinessService;
            _logger = logger;
        }

        [HttpPost]
        [Route("assign-task")]
        public IActionResult RegisterTask([FromBody] Work task) // [FromBody] NodeRegistrationRequest node
        {
             _ = _workerBusinessService.ExecuteTaskAsync(task);
            return Ok();
        }

        [HttpPost]
        [Route("unassigned-task")]
        public IActionResult UnRegisterTask([FromBody] Work task) // [FromBody] NodeRegistrationRequest node
        {
            _ = _workerBusinessService.ExecuteTaskAsync(task);
            return Ok();
        }
    }
}