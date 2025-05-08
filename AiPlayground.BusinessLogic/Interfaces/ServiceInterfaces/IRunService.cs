using AiPlayground.BusinessLogic.DTOs;
namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IRunService
    {
        //Task<IEnumerable<RunDto>> GetAllRunsAsync();
        //Task<RunDto> GetRunByIdAsync(int id);
        Task<List<RunDto>> CreateRunsAsync(RunCreateDto run);
        //Task<RunDto> UpdateRunAsync(RunDto run);
        //Task DeleteRunAsync(int id);

    }
}