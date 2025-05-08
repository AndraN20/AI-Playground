using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.MapperInterfaces
{
    public interface IRunMapper
    {
        public abstract Run toEntity(RunCreateDto runCreateDto);

        public abstract RunDto toDto(Run run);
    }
}
