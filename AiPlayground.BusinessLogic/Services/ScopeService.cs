using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.BusinessLogic.Interfaces.ServiceInterfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
namespace AiPlayground.BusinessLogic.Services
{
    public class ScopeService : IScopeService
    {
        private readonly IRepository<Scope> _scopeRepository;
        private readonly IScopeMapper _scopeMapper;
        private readonly IPromptMapper _promptMapper;
        public ScopeService(IRepository<Scope> scopeRepository, IScopeMapper scopeMapper, IPromptMapper promptMapper)
        {
            _scopeRepository = scopeRepository;
            _scopeMapper = scopeMapper;
            _promptMapper = promptMapper;
        }
        public async Task<IEnumerable<ScopeDto>> GetAllScopesAsync()
        {
            var scopes = await _scopeRepository.GetAllAsync();
            return scopes.Select(_scopeMapper.toDto);
        }
        public async Task<ScopeDto> GetScopeByIdAsync(int id)
        {
           var scope = await _scopeRepository.GetByIdAsync(id);
            if (scope == null)
            {
                throw new Exception($"Scope with ID {id} not found.");
            }
            return _scopeMapper.toDto(scope);
           
        }
        public async Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto)
        {
            var scope = _scopeMapper.toEntity(scopeDto);
            var createdScope = await _scopeRepository.AddAsync(scope);
            return _scopeMapper.toDto(createdScope);
        }
        public async Task<ScopeDto> UpdateScopeAsync(ScopeDto scopeDto)
        {
            var scope = await _scopeRepository.GetByIdAsync(scopeDto.Id);
            if (scope == null)
            {
                throw new Exception($"Scope with ID {scopeDto.Id} not found.");
            }
            await _scopeRepository.UpdateAsync(scope);
            return _scopeMapper.toDto(scope);
        }
        public async Task DeleteScopeAsync(int id)
        {
            var scope = await _scopeRepository.GetByIdAsync(id) ?? throw new Exception($"Scope with ID {id} not found.");
            await _scopeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId)
        {
            var scope = await _scopeRepository.GetByIdAsync(scopeId);
            if (scope == null)
            {
                throw new Exception($"Scope with ID {scopeId} not found.");
            }
            var promptsByScope = scope.Prompts;
            return promptsByScope.Select(_promptMapper.toDto);
        }
    }
}
