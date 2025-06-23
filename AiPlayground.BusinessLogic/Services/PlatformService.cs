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
        private readonly RatingService _ratingService;

        public PlatformService(IRepository<Platform> platformRepository, IPlatformMapper platformMapper, RatingService ratingService)
        {
            _platformRepository = platformRepository;
            _platformMapper = platformMapper;
            _ratingService = ratingService;
        }
        public async Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync()
        {
            var platforms = await _platformRepository.GetAllAsync();
            var platformDtos = platforms.Select(_platformMapper.toDto).ToList();

            foreach (var platformDto in platformDtos)
            {
                if (platformDto.Models != null)
                {
                    foreach (var modelDto in platformDto.Models)
                    {
                        modelDto.AverageRating = await _ratingService.CalculateAverageRatingForModelAsync(modelDto.Id);
                    }
                }
            }


            return platformDtos;
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
