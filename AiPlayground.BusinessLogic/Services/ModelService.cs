using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> _modelRepository;
        private readonly IModelMapper _modelMapper;
        public ModelService(IModelMapper modelMapper, IRepository<Model> modelRepository)
        {
             _modelRepository = modelRepository;
             _modelMapper = modelMapper;
        }

        //public async Task deleteModelAsync(int id)
        //{
        //    var model = await _modelRepository.GetByIdAsync(id);
        //    if (model == null) throw new Exception("Model not found");
        //    await _modelRepository.DeleteAsync(id);
        //}

        public async Task<IEnumerable<ModelDto>> getAllModelsAsync()
        {
            var models = await _modelRepository.GetAllAsync();
            return models.Select(_modelMapper.toDto);
        }

        public async Task<ModelDto> getModelByIdAsync(int id)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model == null) throw new Exception("Model not found");
            return _modelMapper.toDto(model);

        }
        //public async Task<ModelDto> updateModelAsync(ModelDto modelDto)
        //{
        //    var model = await _modelRepository.GetByIdAsync(modelDto.Id);
        //    if (model == null) throw new Exception("Model not found");
        //    var updatedModel = await _modelRepository.UpdateAsync(_modelMapper.toEntity(modelDto));
        //    return _modelMapper.toDto(updatedModel);

        //}
    }
}
