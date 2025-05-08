using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.MapperInterfaces
{
    public interface IModelMapper
    {
        public abstract ModelDto toDto(Model model);
    }
}
