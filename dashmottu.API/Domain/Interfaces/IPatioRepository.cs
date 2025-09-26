using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PatioEntity?> Adicionar(PatioEntity patio);

        Task<PatioEntity?> Atualizar(int id, PatioEntity patio);

        Task<PageResultModel<IEnumerable<PatioResponse?>>> ObterTodos(int deslocamento, int limite);

        Task<PatioResponse?> ObterPorId(int id);

        Task<PatioEntity?> Deletar(int id);
    }
}