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
        //[HttpGet]
        //public async Task<IActionResult> GetRuns()
        //{
        //    var runs = await _runService.GetAllRunsAsync();
        //    return Ok(runs);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateRuns([FromBody] RunCreateDto runCreateDto)
        {
            if (runCreateDto.ModelsToRun.Count == 0)
            {
                return BadRequest("Invalid run data.");
            }

            var runs = await _runService.CreateRunsAsync(runCreateDto);

            return Ok(runs);
        }


        //    return Ok("Run created successfully");
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetRun(int id)
        //{
        //    var run = await _runService.GetRunByIdAsync(id);
        //    if (run == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(run);
        //}
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateRun(int id, [FromBody] double Rating, double UserRating)
        //{
        //    var run = await _runService.GetRunByIdAsync(id);
        //    if (run == null)
        //    {
        //        return NotFound();
        //    }
        //    run.Rating = Rating;
        //    run.UserRating = UserRating;
        //    var updatedRun = await _runService.UpdateRunAsync(run);
        //    return Ok(updatedRun);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRun(int id)
        //{
        //    await _runService.DeleteRunAsync(id);   
        //    return NoContent();
        //}
    }
}
