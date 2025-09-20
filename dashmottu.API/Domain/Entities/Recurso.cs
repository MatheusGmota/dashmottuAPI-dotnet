using dashmottu.API.Application.DTOs;

namespace dashmottu.API.Domain.Entities
{
    public record Recurso
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
