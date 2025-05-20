using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        IEnumerable<PatioCreateDto> ObterTodosPatios();
        PatioCreateDto? AdicionarPatio(PatioCreateDto patio);
        PatioCreateDto? EditarPatio(int id, PatioCreateDto patioAtualizado);
        PatioCreateDto? ObterPatioPorId(int id);
    }
}
