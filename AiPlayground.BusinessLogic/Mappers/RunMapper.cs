using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class RunMapper : IRunMapper
    {
        private readonly IModelMapper _modelMapper;
        private readonly IPromptMapper _promptMapper;
        public RunMapper(IModelMapper modelMapper, IPromptMapper promptMapper)
        {
            _modelMapper = modelMapper;
            _promptMapper = promptMapper;
        }
        public RunDto toDto(Run run)
        {
            return new RunDto
            {
                Id = run.Id,
                ModelId = run.ModelId,
                PromptId = run.PromptId,
                ActualResponse = run.ActualResponse,
                Rating = run.Rating,
                UserRating = run.UserRating,
                Temperature = run.Temperature,
                Model = _modelMapper.toDto(run.Model),
                Prompt = _promptMapper.toDto(run.Prompt)
            };
        }

    }
}
