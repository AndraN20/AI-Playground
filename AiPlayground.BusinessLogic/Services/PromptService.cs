using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services
{
    public class PromptService :IPromptService
    {
        private readonly IRepository<Prompt> _promptRepository;
        private readonly IPromptMapper _promptMapper;
        public PromptService(IRepository<Prompt> promptRepository, IPromptMapper promptMapper)
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
    }
}
