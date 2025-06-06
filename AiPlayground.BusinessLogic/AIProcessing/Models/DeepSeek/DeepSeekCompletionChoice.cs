namespace AiPlayground.BusinessLogic.AIProcessing.Models.DeepSeek
{
    public class DeepSeekCompletionChoice
    {
        public DeepSeekMessage Message { get; set; } = new DeepSeekMessage();

        public string finish_reason { get; set; } = string.Empty;
    }
}
