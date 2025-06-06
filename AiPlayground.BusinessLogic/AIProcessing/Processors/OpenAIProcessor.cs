using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;
using System.Diagnostics;

namespace AiPlayground.BusinessLogic.AIProcessing.Processors
{
    public class OpenAIProcessor : IAIProcessor
    {
        private readonly RatingService _ratingService;


        public OpenAIProcessor(RatingService ratingService)
        {
            _ratingService = ratingService;

        }

        public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
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
                Temperature = (float)temperature
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ChatCompletion requestCompletion = await chatClient.CompleteChatAsync(message, options);
            stopwatch.Stop();
            var elapsed_time = stopwatch.Elapsed.TotalSeconds;

            Debug.WriteLine($"Elapsed time: {elapsed_time} seconds");

            if (requestCompletion == null || requestCompletion.Content == null || !requestCompletion.Content.Any())
            {
                throw new Exception("No response from OpenAI API.");
            }
            var actualResponse = requestCompletion.Content.First().Text;

            var final_rating = await _ratingService.CalculateFinalRatingAsync(prompt.ExpectedResponse, actualResponse, elapsed_time, prompt.SystemMsg); 
            Debug.WriteLine($"Final rating is {final_rating}");

            return new Run
            {
                ModelId = model.Id,
                PromptId = prompt.Id,
                ActualResponse = actualResponse,
                Temperature = temperature,
                Model = model,
                Prompt = prompt,
                Rating = final_rating
            };
        }
    }
}
