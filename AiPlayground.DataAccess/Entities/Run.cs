namespace AiPlayground.DataAccess.Entities
{
    public class Run
    {
        public int ModelId { get; set; }
        public int PromptId { get; set; }

        public int Id { get; set; }

        public string ActualResponse { get; set; } = string.Empty;

        public double Rating { get; set; }

        public double UserRating { get; set; }

        public double Temperature { get; set; }

        public Model Model { get; set; } = null!;

        public Prompt Prompt { get; set; } = null!; 
    }
}
