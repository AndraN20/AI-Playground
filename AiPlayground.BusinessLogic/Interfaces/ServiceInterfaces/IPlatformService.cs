using AiPlayground.BusinessLogic.DTOs;

namespace AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
        Task<PlatformDto> GetPlatformByIdAsync(int id);
        //Task<PlatformDto> UpdatePlatformAsync(PlatformDto platform);
        //Task DeletePlatformAsync(int id);
    }
}
