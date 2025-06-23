using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class PromptMapper : IPromptMapper
    {
        public PromptDto toDto(Prompt prompt)
        {
            return new PromptDto
            {
                Id = prompt.Id,
                UserMessage = prompt.UserMessage,
                Name = prompt.Name,
                SystemMessage = prompt.SystemMessage,
                ExpectedResult = prompt.ExpectedResult
            };
        }

        public Prompt toEntity(PromptCreateDto promptCreateDto)
        {
            return new Prompt
            {
                UserMessage = promptCreateDto.UserMessage,
                Name = promptCreateDto.Name,
                ScopeId = promptCreateDto.ScopeId,
                SystemMessage = promptCreateDto.SystemMessage,
                ExpectedResult = promptCreateDto.ExpectedResult
            };
        }
    }
}
