
using AiPlayground.BusinessLogic.AIProcessing.Models.Gemini;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace AiPlayground.BusinessLogic.AIProcessing.Processors
{
    public class GeminiProcessor : IAIProcessor
    {
        private readonly RatingService _ratingService;
        public GeminiProcessor(RatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public async Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature)
        {
            var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Gemini API key is not set.");
            }

            var requestURL = $"https://generativelanguage.googleapis.com/v1beta/models/{model.Name}:generateContent?key={apiKey}";
            var client = new HttpClient();
            var content = new GeminiRequest {
                Model = model.Name,
                Contents = new List<GeminiContent>
                {
                new GeminiContent
                    {
                    Parts = new List<Part>
                        {
                        new Part { Text = prompt.UserMessage }
                        }
                    }
                },
                SystemInstruction = new SystemInstruction
                {
                    Parts = new List<Part>
                    {
                        new Part{ Text = prompt.SystemMessage }
                    }
                },
                GenerationConfig = new GenerationConfig
                {
                    Temperature = temperature
                }
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

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Gemini API error: {response.StatusCode} - {responseContent}");

                var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var outputText = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                if (string.IsNullOrWhiteSpace(outputText))
                    throw new Exception("Gemini did not return a valid response.");

                var final_rating = await _ratingService.CalculateFinalRatingAsync(prompt.ExpectedResult, outputText, elapsed_time, prompt.SystemMessage);
                Debug.WriteLine($"Final rating is {final_rating}");

                return new Run
                {
                    ModelId = model.Id,
                    PromptId = prompt.Id,
                    ActualResponse = outputText,
                    Temperature = temperature,
                    Rating = final_rating,
                    UserRating = 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing Gemini request: {ex.Message}");
            }
        }
    }
}
