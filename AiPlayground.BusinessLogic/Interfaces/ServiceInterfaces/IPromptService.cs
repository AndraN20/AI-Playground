using AiPlayground.BusinessLogic.DTOs;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IPromptService
    {
        Task<IEnumerable<PromptDto>> GetAllPromptsAsync();
        Task<PromptDto> GetPromptByIdAsync(int id);
        Task<PromptDto> CreatePromptAsync(PromptCreateDto promptDto);
        //Task<PromptDto> UpdatePromptAsync(PromptDto promptDto);
        Task DeletePromptAsync(int id);
    }
}
