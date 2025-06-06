using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.AIProcessing.Processors
{
    public interface IAIProcessor
    {
        public abstract Task<Run> ProcessAsync(Prompt prompt, Model model, float temperature);
    }
}
