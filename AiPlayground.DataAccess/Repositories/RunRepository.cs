

using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories
{
    public class RunRepository : BaseRepository<Run>, IRunRepository
    {
        public RunRepository(AiPlaygroundContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Run>> GetByModelIdAsync(int modelId)
        {
            return await _context.Runs
                                 .Include(r => r.Model)
                                 .Include(r => r.Prompt)
                                 .Where(r => r.ModelId == modelId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Run>> GetByPromptIdAsync(int promptId)
        {
            return await _context.Runs
                .Include(r => r.Model)
                .Include(r => r.Prompt)
                .Where(r => r.PromptId == promptId)
                .ToListAsync();
        }


        public async Task<Run> UpdateAsync(int runId, double rating)
        {
            var run = await _context.Runs
               .Include(r => r.Model)
               .Include(r => r.Prompt)
               .FirstOrDefaultAsync(r => r.Id == runId);
            if (run == null)
            {
                throw new Exception($"Run with ID {runId} not found.");
            }
            run.UserRating = rating;
            _context.Runs.Update(run);
            await _context.SaveChangesAsync();
            return run;
        }

      
        public async Task<IEnumerable<Run>> GetAllAsyncWithModelAndPrompt()
        {
            return await _context.Runs
                .Include(r => r.Model)
                .Include(r => r.Prompt)
                .ToListAsync();
        }
        
    }
}