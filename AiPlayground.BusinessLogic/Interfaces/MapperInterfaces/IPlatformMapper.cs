using AiPlayground.BusinessLogic.DTOs;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces.MapperInterfaces
{
    public interface IPlatformMapper
    {
        public abstract PlatformDto toDto(Platform platform);
    }
}
