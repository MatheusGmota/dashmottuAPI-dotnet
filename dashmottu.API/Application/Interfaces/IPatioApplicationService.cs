using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        PatioCreateDto? AdicionarPatio(PatioCreateDto patio);
    }
}
