namespace AiPlayground.BusinessLogic.DTOs
{
    public class PromptDto
    {
        public int Id { get; set; }
        public string UserMessage { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SystemMsg { get; set; } = string.Empty;
        public string ExpectedResponse { get; set; } = string.Empty;
    }
}
