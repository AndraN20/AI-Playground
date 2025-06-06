using AiPlayground.BusinessLogic.AIProcessing.Models.DeepSeek;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess.Entities;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;


namespace AiPlayground.BusinessLogic.AIProcessing.Processors
{
    public class DeepseekProcessor : IAIProcessor
    {
        private readonly RatingService _ratingService;
        public DeepseekProcessor(RatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
        {
            var apiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Deepseek API key is not set.");
            }
            var requestURL = "https://api.deepseek.com/chat/completions";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var content = new DeepSeekRequest
            {
                Model = model.Name,
                Messages =
                [
                    new DeepSeekMessage
                    {
                        Role = "system",
                        Content = prompt.SystemMsg
                    },
                    new DeepSeekMessage
                    {

                        Role = "user",
                        Content = prompt.UserMessage
                    }
                ],
                Temperature = temperature,
                Stream = false,
            };

            var jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var response = await client.PostAsync(requestURL, httpContent);

                stopwatch.Stop();
                var elapsed_time = stopwatch.Elapsed.TotalSeconds;

                var responseContent = await response.Content.ReadAsStringAsync();
                var deepSeekResponse = JsonSerializer.Deserialize<DeepSeekResponse>(responseContent, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase    
                });


                if (deepSeekResponse != null)
                {
                    var final_rating = await _ratingService.CalculateFinalRatingAsync(prompt.ExpectedResponse, deepSeekResponse.Choices.First().Message.Content, elapsed_time,prompt.SystemMsg);
                    Debug.WriteLine($"Final rating is {final_rating}");

                    return new Run
                    {
                        ModelId = model.Id,
                        PromptId = prompt.Id,
                        ActualResponse = deepSeekResponse.Choices.First().Message.Content,
                        Temperature = temperature,
                        Rating = final_rating,
                        UserRating = 0
                    };
                }
                else
                { throw new Exception("DeepSeek response is null"); }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing deepseek request: {ex.Message}");
            }

        }
    }
}
