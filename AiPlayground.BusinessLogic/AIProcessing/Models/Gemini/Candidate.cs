
namespace AiPlayground.BusinessLogic.AIProcessing.Models.Gemini
{
    public class Candidate
    {
        public GeminiContent Content {  get; set; } = new GeminiContent();
        public string finish_reason { get; set; } = string.Empty;
    }
}
