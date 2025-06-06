using AiPlayground.BusinessLogic.AIProcessing.Processors;
using AiPlayground.BusinessLogic.Enums;

namespace AiPlayground.BusinessLogic.AIProcessing.Factories
{
    public interface IAIProcessorFactory
    {
        IAIProcessor CreateAIProcessor(PlatformType platformType);
    }
}
