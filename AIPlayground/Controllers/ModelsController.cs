using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels()
        {
            var models = await _modelService.getAllModelsAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getModel (int id)
        {
            var model = await _modelService.getModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

       

    }
}
