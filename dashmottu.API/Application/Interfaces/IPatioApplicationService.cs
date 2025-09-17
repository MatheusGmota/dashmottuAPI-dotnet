using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        Task<IEnumerable<PatioResponse>> ObterTodosPatios();
        Task<PatioResponse?> AdicionarPatio(PatioRequest entidade);
        Task<PatioResponse?> EditarPatio(int id, PatioRequest entidade);
        Task<PatioResponse?> ObterPatioPorId(int id);
        Task<PatioEntity?> DeletarPatio(int id);
    }
}
