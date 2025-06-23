using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;
using System.Globalization;

namespace AiPlayground.BusinessLogic.Services
{
    public class RatingService
    {
        private readonly ChatClient _chatClient;
        private readonly IRunRepository _runRepository;

        public RatingService(IRunRepository runRepository)
        {
            _chatClient = new ChatClient(model: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
            _runRepository = runRepository;
        }

        public async Task<double> CalculateFinalRatingAsync(string expected, string actual, double elapsed_time, string systemMsg)
        {

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(
                    "Evalueaza diferenta dintre 'expected response' si 'actual response pentru cele 2 string uri de mai jos." +
                     "Tine cont de sens, corectitudine semantica si gramaticala, de cat de complet e raspunsul si relevanta." +
                     "Tine cont si de scope ul 'scope' in care se afla raspunsul." +
                     "Da un scor intre 0.00 si 100.00 si doar atat in raspuns, nimic mai mult. 0 este pentru cea mai mica relevanta intre cele 2 string uri, 100 pentru cea mai ridicata." +
                     "Tine cont si de 'system message' cand dai raspunsul"
                ),
                new UserChatMessage($"Expected Response:\n\"{expected}\"\n\nActual Response:\n\"{actual}\"\n\nSystem Message:\n\"{systemMsg}\"")
            };

            var options = new ChatCompletionOptions
            {
                Temperature = 0.0f,
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages, options);
            if (completion == null || completion.Content == null || !completion.Content.Any())
            {
                throw new Exception("No response from OpenAI API.");
            }

            var content = completion.Content.First().Text;

            if (string.IsNullOrEmpty(content))
                throw new Exception("No rating returned from GPT.");

            double score = double.Parse(content, CultureInfo.InvariantCulture);

            return Math.Clamp(score, 0.0, 100.0) - elapsed_time;
        }

        public async Task<float> CalculateAverageRatingForModelAsync(int modelId)
        {
            var runs = await _runRepository.GetByModelIdAsync(modelId);
            var modelRuns = runs.Where(r => r.ModelId == modelId && r.UserRating > 0 && r.Rating > 0);
            if (!modelRuns.Any()) return 0;

            return (float)modelRuns.Average(r => (r.UserRating + r.Rating) / 2f);
        }

    }
}
