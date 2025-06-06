using AiPlayground.DataAccess.Entities;

namespace AiPlayground.DataAccess.Repositories
{
    public interface IPromptRepository : IRepository<Prompt>
    {
        Task<IEnumerable<Prompt>> GetPromptsByScopeIdAsync(int scopeId);


    }
}
