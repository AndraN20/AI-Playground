using AiPlayground.BusinessLogic.DTOs;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IScopeService
    {
        Task<IEnumerable<ScopeDto>> GetAllScopesAsync();
        Task<ScopeDto> GetScopeByIdAsync(int id);
        Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto);
        Task<ScopeDto> UpdateScopeAsync(ScopeDto scopeDto);
        Task DeleteScopeAsync(int id);
        Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId);
    }
}