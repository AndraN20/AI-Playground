using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly IScopeService _scopeService;
        public ScopesController(IScopeService scopeService) {
            _scopeService = scopeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetScopes()
        {
            var scopes = await _scopeService.GetAllScopesAsync();
            return Ok(scopes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScope(int id)
        {
            var scope = await _scopeService.GetScopeByIdAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return Ok(scope);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScope([FromBody] ScopeCreateDto scopeDto)
        {
            if (scopeDto == null)
            {
                return BadRequest("Scope data is null.");
            }
            var createdScope = await _scopeService.CreateScopeAsync(scopeDto);
            return CreatedAtAction(nameof(GetScope), new { id = createdScope.Id }, createdScope);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScope(int id, [FromBody] ScopeDto scopeDto)
        {
            if (scopeDto == null || id != scopeDto.Id)
            {
                return BadRequest("Scope data is invalid.");
            }
            var updatedScope = await _scopeService.UpdateScopeAsync(scopeDto);
            if (updatedScope == null)
            {
                return NotFound();
            }
            return Ok(updatedScope);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScope(int id)
        {
            try
            {
                await _scopeService.DeleteScopeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




    }
}
