using AiPlayground.BusinessLogic.DTOs;
namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IRunService
    {

        Task<RunDto> UpdateRunAsync(int runId, double userRating);

        Task<IEnumerable<RunDto>> GetRunsByPromptAsync(int promptId);
        Task<IEnumerable<RunDto>> GetRunsByModelAsync(int modelId);

        Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto);
    }
}