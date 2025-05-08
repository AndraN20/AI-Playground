using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services.RunCreationStrategy
{
    public class DeepSeekCreationStrategy : IRunCreationStrategy
    {
        private readonly IRepository<Run> _runRepository;
        private readonly IRunMapper _runMapper;

        public DeepSeekCreationStrategy(IRepository<Run> runRepository, IRunMapper runMapper)
        {
            _runRepository = runRepository;
            _runMapper = runMapper;
        }

        public PlatformType PlatformType => PlatformType.DeepSeek;

        public async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, double temperature)
        {

            var actualResponse = "DeepSeek response"; 

            var run = await CreateRun(model.Id, prompt.Id, actualResponse, temperature, 0);
            return _runMapper.toDto(run);
        }

        private async Task<Run> CreateRun(int modelId, int promptId, string actualResponse, double temperature, double rating)
        {
            var run = new Run
            {
                ModelId = modelId,
                PromptId = promptId,
                ActualResponse = actualResponse,
                Temperature = temperature,
                Rating = rating
            };
            await _runRepository.AddAsync(run);
            return run;
        }
    }
}
