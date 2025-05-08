using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;

namespace AiPlayground.BusinessLogic.Services.RunCreationStrategy
{
    public class OpenAICreationStrategy : IRunCreationStrategy
    {
        private readonly IRepository<Run> _runRepository;
        private readonly IRunMapper _runMapper;

        public OpenAICreationStrategy(IRepository<Run> runRepository, IRunMapper runMapper)
        {
            _runRepository = runRepository;
            _runMapper = runMapper;
        }

        public PlatformType PlatformType => PlatformType.OpenAI;

        public async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, double temperature)
        {
            ChatClient chatClient = new(model: model.Name, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

            var systemMessage = new SystemChatMessage(prompt.SystemMsg);
            var userMessage = new UserChatMessage(prompt.UserMessage);

            var message = new List<ChatMessage> { systemMessage, userMessage };
            var options = new ChatCompletionOptions { Temperature = (float)temperature };

            ChatCompletion requestCompletion = await chatClient.CompleteChatAsync(message, options);
            var actualResponse = requestCompletion.Content.First().Text;

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
