﻿

namespace AiPlayground.BusinessLogic.DTOs
{
    public class PlatformDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public List<ModelDto> Models { get; set; } = null!;

    }
}
