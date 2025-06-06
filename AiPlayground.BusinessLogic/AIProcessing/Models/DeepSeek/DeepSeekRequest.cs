namespace AiPlayground.BusinessLogic.AIProcessing.Models.DeepSeek
{
    public class DeepSeekRequest
    {
        public string Model { get; set; } = string.Empty;

        public List<DeepSeekMessage> Messages { get; set; } = [];

        public float Temperature { get; set; } = 1f;

        public bool Stream { get; set; } = false;
    }
}
