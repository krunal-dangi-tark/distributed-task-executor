using Microsoft.AspNetCore.Mvc;
using TaskExecutor.BusinessServices;
using TaskExecutor.Models;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private NodeBusinessService _nodeBusinessService;

        public NodesController(NodeBusinessService nodeBusinessService)
        {
            _nodeBusinessService = nodeBusinessService;
        }

        [HttpGet]
        public List<Node> GetAllNodes() 
        {
            return _nodeBusinessService.GetAllNodes();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNode([FromBody] NodeRegistrationRequest node)
        {
            _nodeBusinessService.RegisterNode(node);
            return Ok();
        }

        [HttpDelete]
        [Route("unregister/{name}")]
        public IActionResult UnRegisterNode(string name)
        {
            _nodeBusinessService.UnregisterNode(name);
            return Ok();
        }        
    }
}