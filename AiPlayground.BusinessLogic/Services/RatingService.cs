using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;
using System.Globalization;

namespace AiPlayground.BusinessLogic.Services
{
    public class RatingService
    {
        //public int ComputeLevenshteinDistance(string source, string target)
        //{
        //    if (source == null || target == null) return 0;
        //    if (source.Length == 0 || target.Length == 0) return 0;
        //    if (source == target) return source.Length;

        //    int sourceWordCount = source.Length;
        //    int targetWordCount = target.Length;

        //    // Step 1
        //    if (sourceWordCount == 0)
        //        return targetWordCount;

        //    if (targetWordCount == 0)
        //        return sourceWordCount;

        //    int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

        //    // Step 2
        //    for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
        //    for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

        //    for (int i = 1; i <= sourceWordCount; i++)
        //    {
        //        for (int j = 1; j <= targetWordCount; j++)
        //        {
        //            // Step 3
        //            int cost = target[j - 1] == source[i - 1] ? 0 : 1;

        //            // Step 4
        //            distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
        //        }
        //    }

        //    return distance[sourceWordCount, targetWordCount];
        //}
        //public double CalculateSimilarity(string source, string target)
        //{
        //    if (source == null || target == null) return 0.0;
        //    if (source.Length == 0 || target.Length == 0) return 0.0;
        //    if (source == target) return 1.0;

        //    var ratingService = new RatingService();
        //    int stepsToSame = ratingService.ComputeLevenshteinDistance(source, target);
        //    return 1.0 - stepsToSame / (double)Math.Max(source.Length, target.Length);
        //}
        //public double CalculateTimePercentage(long elapsedMilliseconds)
        //{
        //    var totalSeconds = 100;
        //    if (elapsedMilliseconds / 1000 <= 0) return 0.0;
        //    if (elapsedMilliseconds / 1000 >= totalSeconds) return 100.0;
        //    return (elapsedMilliseconds / 1000 / (double)totalSeconds) * 100.0;
        //}

        //public double CalculateFinalRating(string source, string target,long elapsedMilliseconds)
        //{
        //    var calculated_rating = CalculateSimilarity(source, target);
        //    var final_rating = calculated_rating * 100 - CalculateTimePercentage(elapsedMilliseconds);
        //    return final_rating;
        //}


        private readonly ChatClient _chatClient;


        public RatingService()
        {
            _chatClient = new ChatClient(model: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
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
    }
}
