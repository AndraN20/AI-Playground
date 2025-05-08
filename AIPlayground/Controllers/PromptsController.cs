using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptsController : ControllerBase
    {
        private readonly IPromptService _promptService;
        public PromptsController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrompts()
        {
            var prompts = await _promptService.GetAllPromptsAsync();
            return Ok(prompts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrompt(int id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound();
            }
            return Ok(prompt);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrompt([FromBody] PromptCreateDto promptDto)
        {
            if (promptDto == null)
            {
                return BadRequest("Prompt data is null.");
            }
            var createdPrompt = await _promptService.CreatePromptAsync(promptDto);
            return CreatedAtAction(nameof(GetPrompt), new { id = createdPrompt.Id }, createdPrompt);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            try
            {
                await _promptService.DeletePromptAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
    
}
