using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.MapperInterfaces
{
    public interface IScopeMapper
    {
        public abstract Scope toEntity(ScopeCreateDto scopeCreateDto);

        public abstract Scope toEntity(ScopeDto scopeDto);

        public abstract ScopeDto toDto(Scope scope);
    }
}
