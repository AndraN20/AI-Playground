namespace AiPlayground.DataAccess.Entities
{
    public class Prompt
    {
        public int Id { get; set; }
        public string UserMessage { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SystemMessage { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;

        public int ScopeId { get; set; }

        public virtual Scope Scope { get; set; } = null!;

        public ICollection<Run> Runs { get; set; } = new HashSet<Run>();

    }
}
