using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services
{
    public class PromptService :IPromptService
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IPromptMapper _promptMapper;
        public PromptService(IPromptRepository promptRepository, IPromptMapper promptMapper)
        {
            _promptRepository = promptRepository;
            _promptMapper = promptMapper;
        }
        public async Task<IEnumerable<PromptDto>> GetAllPromptsAsync()
        {
            var prompts = await _promptRepository.GetAllAsync();
            return prompts.Select(_promptMapper.toDto);
        }
        public async Task<PromptDto> GetPromptByIdAsync(int id)
        {
            var prompt = await _promptRepository.GetByIdAsync(id);
            if (prompt == null)
            {
                throw new Exception($"Prompt with ID {id} not found.");
            }
            return _promptMapper.toDto(prompt);
        }

        public async Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int id)
        {
            var prompts = await _promptRepository.GetPromptsByScopeIdAsync(id);
            if (prompts == null)
            {
                throw new Exception($"No prompts found for scope ID {id}.");
            }
            return prompts.Select(_promptMapper.toDto);
        }

        public async Task<PromptDto> CreatePromptAsync(PromptCreateDto promptDto)
        {
            var promptEntity = _promptMapper.toEntity(promptDto);
            return _promptMapper.toDto(await _promptRepository.AddAsync(promptEntity));
        }

        public async Task DeletePromptAsync(int id)
        {
            var prompt = await _promptRepository.GetByIdAsync(id);
            if (prompt == null)
            {
                throw new Exception($"Prompt with ID {id} not found.");
            }
            await _promptRepository.DeleteAsync(id);
        }

        public async Task<PromptDto> UpdatePromptAsync(int id,PromptCreateDto promptUpdateDto)
        {
            var prompt = await _promptRepository.GetByIdAsync(id);
            if (prompt == null)
            {
                throw new Exception($"Prompt with ID {id} not found.");
            }
            if (!string.IsNullOrEmpty(promptUpdateDto.UserMessage))
                prompt.UserMessage = promptUpdateDto.UserMessage;
            if (!string.IsNullOrEmpty(promptUpdateDto.Name))
                prompt.Name = promptUpdateDto.Name;
            if (!string.IsNullOrEmpty(promptUpdateDto.SystemMessage))
                prompt.SystemMessage = promptUpdateDto.SystemMessage;
            if (!string.IsNullOrEmpty(promptUpdateDto.ExpectedResult))
                prompt.ExpectedResult = promptUpdateDto.ExpectedResult;

            var updatedPrompt = await _promptRepository.UpdateAsync(prompt);
            return _promptMapper.toDto(updatedPrompt);
        }
    }
}
