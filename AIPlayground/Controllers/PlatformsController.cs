using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            var platforms = await _platformService.GetAllPlatformsAsync();
            return Ok(platforms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatform(int id)
        {
            var platform = await _platformService.GetPlatformByIdAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            return Ok(platform);
        }
    }
}
