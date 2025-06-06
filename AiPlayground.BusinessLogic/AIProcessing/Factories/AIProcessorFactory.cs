using AiPlayground.BusinessLogic.AIProcessing.Processors;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Services;

namespace AiPlayground.BusinessLogic.AIProcessing.Factories
{
    public class AIProcessorFactory : IAIProcessorFactory
    {
        private readonly RatingService _ratingService;

        public AIProcessorFactory(RatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public IAIProcessor CreateAIProcessor(PlatformType platformType)
        {
            switch(platformType)
            {
                case PlatformType.OpenAI:
                    return new OpenAIProcessor(_ratingService);
                case PlatformType.DeepSeek:
                    return new DeepseekProcessor(_ratingService);
                case PlatformType.Gemini:
                    return new GeminiProcessor(_ratingService);

                default:
                    throw new NotImplementedException($"Processor for platform type {platformType} is not implemented.");
            }
        }
    }
}
