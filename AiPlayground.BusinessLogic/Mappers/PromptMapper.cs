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
                SystemMsg = prompt.SystemMsg,
                ExpectedResponse = prompt.ExpectedResponse
            };
        }

        public Prompt toEntity(PromptCreateDto promptCreateDto)
        {
            return new Prompt
            {
                UserMessage = promptCreateDto.UserMessage,
                Name = promptCreateDto.Name,
                ScopeId = promptCreateDto.ScopeId,
                SystemMsg = promptCreateDto.SystemMessage,
                ExpectedResponse = promptCreateDto.ExpectedResult
            };
        }
    }
}
