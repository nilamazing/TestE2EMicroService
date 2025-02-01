using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunicationController: ControllerBase
    {
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(ILogger<CommunicationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("platforms/ping")]
        public ActionResult PostFromPlatform()
        {
            _logger.LogInformation("Pinged");
            return Ok("Ping");
        }
    }
}
