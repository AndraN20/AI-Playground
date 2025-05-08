using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IRepository<Platform> _platformRepository;
        private readonly IPlatformMapper _platformMapper;

        public PlatformService(IRepository<Platform> platformRepository, IPlatformMapper platformMapper)
        {
            _platformRepository = platformRepository;
            _platformMapper = platformMapper;
        }
        public async Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync()
        {
            var platforms = await _platformRepository.GetAllAsync();
            return platforms.Select(_platformMapper.toDto);
        }
        public async Task<PlatformDto> GetPlatformByIdAsync(int id)
        {
            var platform = await _platformRepository.GetByIdAsync(id);
            if (platform == null)
            {
                throw new Exception($"Platform with ID {id} not found.");
            }
            return _platformMapper.toDto(platform);
        } 
    }
}
