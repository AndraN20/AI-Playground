using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.MapperInterfaces
{
    public interface IPromptMapper
    {
        public abstract Prompt toEntity(PromptCreateDto promptCreateDto);

        public abstract PromptDto toDto(Prompt prompt);
    }
}
