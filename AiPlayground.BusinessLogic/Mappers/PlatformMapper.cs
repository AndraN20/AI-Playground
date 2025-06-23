using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class PlatformMapper : IPlatformMapper
    {
        private readonly IModelMapper _modelMapper;

        public PlatformMapper(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public PlatformDto toDto(Platform platform)
        {
            return new PlatformDto
            {
                Id = platform.Id,
                Name = platform.Name,
                BaseUrl = platform.ImageUrl,
                Models = (platform.Models ?? new List<Model>())
                             .Select(m => new ModelDto
                             {
                                 Id = m.Id,
                                 Name = m.Name,
                                 AverageRating = 0 
                             })
                             .ToList()

            };
        }
    }
}
