using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;

namespace AiPlayground.BusinessLogic.Services
{
    public class RunService : IRunService
    {
        private readonly IRepository<Run> _runRepository;
        private readonly IRepository<Model> _modelRepository;
        private readonly IRepository<Prompt> _promptRepository;
        private readonly IRunMapper _runMapper;
        private readonly IEnumerable<IRunCreationStrategy> _platformStrategy;
        public RunService(IRepository<Run> runRepository, IRepository<Prompt> promptRepository, IRepository<Model> modelRepository, IRunMapper runMapper, IEnumerable<IRunCreationStrategy> strategy)
        {
            _runRepository = runRepository;
            _modelRepository = modelRepository;
            _promptRepository = promptRepository;
            _runMapper = runMapper;
            _platformStrategy = strategy;
        }
        public async Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto)
        {
            var runs = new List<RunDto>();
            var prompt = await _promptRepository.GetByIdAsync(runCreateDto.PromptId);
            if (prompt == null)
            {
                throw new Exception($"Prompt with ID {runCreateDto.PromptId} not found.");
            }
            foreach (var modelToRun in runCreateDto.ModelsToRun)
            {
                var model = await _modelRepository.GetByIdAsync(modelToRun.ModelId);
                if (model == null)
                {
                    throw new Exception($"Model with ID {modelToRun.ModelId} not found.");
                }
                var platformType = (PlatformType)model.PlatformId;
                var strategy = _platformStrategy.FirstOrDefault(x => x.PlatformType == platformType);
                if (strategy == null)
                {
                    throw new Exception($"Strategy for platform type {platformType} not found.");
                }

                
                
                var run = await strategy.CreateRunAsync(model, prompt, modelToRun.Temperature);
                runs.Add(run);
                
            }
            return runs;
        }

        private async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, double temperature)
        {
            ChatClient chatClient = new(model: model.Name, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
            
            var systemMessage = new SystemChatMessage(prompt.SystemMsg);
            var userMessage = new UserChatMessage(prompt.UserMessage);

            var message = new List<ChatMessage>
            {
                systemMessage,
                userMessage
            };
            var options = new ChatCompletionOptions
            {
                Temperature = (float)temperature,
            };
            ChatCompletion requestCompletion = await chatClient.CompleteChatAsync(message, options);
            var actualResponse = requestCompletion.Content.First().Text;

            // Cum ati calcula ratingul?
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
