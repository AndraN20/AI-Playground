using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories
{
    public class PromptRepository : BaseRepository<Prompt>, IPromptRepository
    {
        public PromptRepository(AiPlaygroundContext context) : base(context) { }

        public async Task<IEnumerable<Prompt>> GetPromptsByScopeIdAsync(int scopeId)
        {
            var prompts = await _context.Prompts
                .Include(p => p.Scope)
                .Where(p => p.ScopeId == scopeId)
                .ToListAsync();
            return prompts;
        }
        

    }
}
