
namespace AiPlayground.BusinessLogic.AIProcessing.Models.Gemini
{
    public class GeminiRequest
    {
        public string Model { get; set; } = string.Empty;

        public SystemInstruction SystemInstruction { get; set; } = new SystemInstruction();

        public GenerationConfig GenerationConfig { get; set; } = new GenerationConfig();

        public List<GeminiContent> Contents { get; set; } = [];
    }
}
