using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class ModelMapper : IModelMapper
    {

        

        public ModelDto toDto(Model model)
        {
            return new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                AverageRating = 0,
            };
        }
    }
}
