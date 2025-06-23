using AiPlayground.BusinessLogic.AIProcessing.Factories;
using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services
{
    public class RunService : IRunService
    {
        private readonly IRunRepository _runRepository;
        private readonly IRepository<Model> _modelRepository;
        private readonly IRepository<Prompt> _promptRepository;
        private readonly IRunMapper _runMapper;
        private readonly IAIProcessorFactory iAIProcessorFactory;
        private readonly RatingService _ratingService;

        public RunService(IRunRepository runRepository, IRepository<Prompt> promptRepository, IRepository<Model> modelRepository, IRunMapper runMapper, IAIProcessorFactory aiProcessorFactory, RatingService ratingService)
        {
            _runRepository = runRepository;
            _modelRepository = modelRepository;
            _promptRepository = promptRepository;
            _runMapper = runMapper;
            iAIProcessorFactory = aiProcessorFactory;
            _ratingService = ratingService;
        }
        public async Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto)
        {
            var runs = new List<RunDto>();
            var prompt = await _promptRepository.GetByIdAsync(runCreateDto.PromptId);
            if (prompt == null)
            {
                throw new Exception($"Prompt with ID {runCreateDto.PromptId} not found.");
            }


            foreach (var modelToRun in runCreateDto.ModelsToRun)
            {
                var model = await _modelRepository.GetByIdAsync(modelToRun.ModelId);
                if (model == null)
                {
                    throw new Exception($"Model with ID {modelToRun.ModelId} not found.");
                }
                var platformType = (PlatformType)model.PlatformId;

                var aiProcessor = iAIProcessorFactory.CreateAIProcessor(platformType);
                var run = await aiProcessor.ProcessAsync(prompt, model,modelToRun.Temperature);

                var createdRun = await CreateRun(run);
                var runDto = _runMapper.toDto(createdRun);
    



                runs.Add(runDto);
            }
            return runs;
        }

        public async Task<IEnumerable<RunDto>> GetRunsByModelAsync(int modelId)
        {
            var runs = await _runRepository.GetByModelIdAsync(modelId);
            return runs.Select(_runMapper.toDto);
        }

        public async Task<IEnumerable<RunDto>> GetRunsByPromptAsync(int promptId)
        {
            var runs = await _runRepository.GetByPromptIdAsync(promptId);
            return runs.Select(_runMapper.toDto);
        }

        public async Task<RunDto> UpdateRunAsync(int runId, double rating)
        {
            var run = await _runRepository.UpdateAsync(runId, rating);
            if (run == null)
            {
                throw new Exception($"Run with ID {runId} not found.");
            }
            return _runMapper.toDto(run);
        }


        private async Task<Run> CreateRun(Run run)
        {          
            var saved_run = await _runRepository.AddAsync(run);
            if(saved_run == null)
            {
                throw new Exception("failed to save the run.");
            }

            return run;
        }

        public async Task<IEnumerable<RunDto>> GetAllRuns()
        {
           var runs = await _runRepository.GetAllAsyncWithModelAndPrompt();
            if (runs == null || !runs.Any())
            {
                throw new Exception("No runs found.");
            }
            return runs.Select(_runMapper.toDto);
        }


    }
}
