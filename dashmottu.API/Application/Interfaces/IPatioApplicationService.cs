using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        Task<IEnumerable<PatioCreateDto>> ObterTodosPatios();
        Task<PatioCreateDto?> AdicionarPatio(PatioCreateDto patio);
        Task<PatioCreateDto?> EditarPatio(int id, PatioCreateDto patioAtualizado);
        Task<PatioCreateDto?> ObterPatioPorId(int id);
        Task<PatioEntity?> DeletarPatio(int id);
        Task<LoginResponseDto> ValidarLogin(LoginDto login);
    }
}
