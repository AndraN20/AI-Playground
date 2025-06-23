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
        private readonly RatingService _ratingService;
        public ModelService(IModelMapper modelMapper, IRepository<Model> modelRepository, RatingService ratingService)
        {
             _modelRepository = modelRepository;
             _modelMapper = modelMapper;
            _ratingService = ratingService;
        }

        public async Task<IEnumerable<ModelDto>> getAllModelsAsync()
        {
            var models = await _modelRepository.GetAllAsync();

            var dtoList = new List<ModelDto>();
            foreach (var model in models)
            {
                var avgRating = await _ratingService.CalculateAverageRatingForModelAsync(model.Id);
                dtoList.Add(new ModelDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    AverageRating = avgRating
                });
            }

            return dtoList;
        }


        public async Task<ModelDto> getModelByIdAsync(int id)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model == null) throw new Exception("Model not found");
            var avgRating = await _ratingService.CalculateAverageRatingForModelAsync(model.Id);

            return new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                AverageRating = avgRating
            };

        }
      
    }
}
