using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class RunMapper : IRunMapper
    {
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
                Temperature = run.Temperature

            };
        }
        public Run toEntity(RunCreateDto runCreateDto)
        {
            return new Run
            {
                PromptId = runCreateDto.PromptId

            };
        }
    }
}
