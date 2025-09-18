using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;
using dashmottu.API.Domain.Model;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        Task<PaginacaoModel<IEnumerable<PatioResponse?>>> ObterTodosPatios(int deslocamento, int limite);
        Task<PatioResponse?> AdicionarPatio(PatioRequest entidade);
        Task<PatioResponse?> EditarPatio(int id, PatioRequest entidade);
        Task<PatioResponse?> ObterPatioPorId(int id);
        Task<PatioEntity?> DeletarPatio(int id);
    }
}
