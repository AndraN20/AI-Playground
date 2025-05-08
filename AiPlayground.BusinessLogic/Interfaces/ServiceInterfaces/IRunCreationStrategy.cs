using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IRunCreationStrategy
    {
        PlatformType PlatformType { get; }
        Task<RunDto> CreateRunAsync(Model model, Prompt prompt, double temperature);
    }
}
