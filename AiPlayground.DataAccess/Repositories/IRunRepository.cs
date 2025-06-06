

using AiPlayground.DataAccess.Entities;

namespace AiPlayground.DataAccess.Repositories
{
    public interface IRunRepository : IRepository<Run>
    {
        Task<IEnumerable<Run>> GetByPromptIdAsync(int promptId);
        Task<IEnumerable<Run>> GetByModelIdAsync(int modelId);

        Task<Run> UpdateAsync(int runId, double rating);
    }
}
