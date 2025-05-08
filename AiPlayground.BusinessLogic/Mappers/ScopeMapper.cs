using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.BusinessLogic.Interfaces.MapperInterfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Mappers
{
    public class ScopeMapper : IScopeMapper
    {
        public Scope toEntity(ScopeCreateDto scopeCreateDto) {
            return new Scope
            {
                Name = scopeCreateDto.Name,
            };
        }

        public Scope toEntity(ScopeDto scopeDto)
        {
            return new Scope
            {
                Id = scopeDto.Id,
                Name = scopeDto.Name
            }; 
        }

        public ScopeDto toDto(Scope scope)
        {
            return new ScopeDto
            {
                Id = scope.Id,
                Name = scope.Name
            };
        }


    }
}
