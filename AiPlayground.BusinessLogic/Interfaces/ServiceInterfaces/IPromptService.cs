using AiPlayground.BusinessLogic.DTOs;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IPromptService
    {
        Task<IEnumerable<PromptDto>> GetAllPromptsAsync();
        Task<PromptDto> GetPromptByIdAsync(int id);

        Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int id);
        Task<PromptDto> CreatePromptAsync(PromptCreateDto promptDto);
        Task<PromptDto> UpdatePromptAsync(int id, PromptCreateDto promptUpdateDto);
        Task DeletePromptAsync(int id);
    }
}
