using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Interfaces
{
    public interface IPatioApplicationService
    {
        Task<OperationResult<PageResultModel<IEnumerable<PatioResponse?>>>> ObterTodosPatios(int deslocamento, int limite);
        Task<OperationResult<PatioResponse?>> AdicionarPatio(PatioRequest entidade);
        Task<OperationResult<PatioResponse?>> EditarPatio(int id, PatioRequest entidade);
        Task<OperationResult<PatioResponse?>> ObterPatioPorId(int id);
        Task<OperationResult<PatioEntity?>> DeletarPatio(int id);
    }
}
