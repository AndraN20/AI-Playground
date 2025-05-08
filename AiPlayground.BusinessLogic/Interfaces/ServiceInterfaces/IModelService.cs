using AiPlayground.BusinessLogic.DTOs;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelDto>> getAllModelsAsync();
        Task<ModelDto> getModelByIdAsync(int id);
        //Task<ModelDto> updateModelAsync(ModelDto model);
        //Task deleteModelAsync(int id);
    }
}
