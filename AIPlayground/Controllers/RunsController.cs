using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunsController : ControllerBase
    {
        private readonly IRunService _runService;
        public RunsController(IRunService runService)
        {
            _runService = runService;
        }
  
        [HttpPost]
        public async Task<IActionResult> CreateRuns([FromBody] RunCreateDto runCreateDto)
        {
           
            var runs = await _runService.CreateRunsAsync(runCreateDto);

            return Ok(runs);
        }

        [HttpGet("/api/models/{id}/runs")]
        public async Task<IActionResult> GetRunsByModelId(int id)
        {
            var runs = await _runService.GetRunsByModelAsync(id);
            return Ok(runs);
        }

        [HttpGet("/api/prompts/{id}/runs")]
        public async Task<IActionResult> GetRunsByPromptId(int id)
        {
            var runs = await _runService.GetRunsByPromptAsync(id);
            return Ok(runs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRuns()
        {
            var runs = await _runService.GetAllRuns();
            return Ok(runs);
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRunAsync(int id, [FromBody]RunRateDto runDto)
        {
            
            var updatedRun = await _runService.UpdateRunAsync(id, runDto.UserRating);
            if (updatedRun == null)
            {
                return NotFound();
            }
            return Ok(updatedRun);
        }
    }
}
